using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithController : MonoBehaviour
{
    //애니메이션
    readonly int c_animHashKeyState = Animator.StringToHash("State");
    readonly int c_animHashKeyDie = Animator.StringToHash("Die");
    [SerializeField] State m_state = State.Idle;
    Animator m_animator;

    //행동, 물리
    public int m_jumpcount = 0;
    int m_maxJumpcount = 2;//몇 단 점프 가능한지
    public float m_jumpPower = 10; //점프파워
    public float m_gravity;   //중력
    [SerializeField] float m_speed = 5f;

    bool m_jumpinspector = false;
    public bool Jumpinspector { set { m_jumpinspector = value; } } //move 함수에서 같이 처리하려고함 불값을 통해서

    enum State : int
    {
        Idle = 0,
        Run = 1,
        Back = 2,
        Left = 3,
        Right = 4,
        Jump = 5
    }

    public CharacterController m_characterController;//character controller이용
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>(); //나의 컴포넌트 중에 character controller를 가져온다
    }

    void ChangeState(State state)
    {
        if (IsState(state))
            return;
        m_state = state;

        m_animator.SetInteger(c_animHashKeyState, (int)m_state);


        //{{상태가 바뀔 때 한번만 처리되어야 하는 것들은 여기서 처리!
        switch (m_state)
        {
            case State.Idle:
                break;
            case State Run:
                break;
        }
        //}}
    }

    bool IsState(State state)
    {
        return m_state == state;
    }
    public void SetYawAngle(float yaw)
    {
        Vector3 angle = transform.eulerAngles;
        angle.y = yaw;
        transform.eulerAngles = angle;
    }

    public void Move(Vector3 localDir)
    {
        ChangeState(State.Run);
        SetRunAnimProperty(localDir.x, localDir.z);

        localDir = transform.TransformDirection(localDir);

        m_characterController.Move(localDir.normalized * m_speed * Time.deltaTime);
    }

    public void MoveEnd()
    {
        ChangeState(State.Idle);
        SetRunAnimProperty(0f, 0f);
    }

    void SetRunAnimProperty(float horizontal, float vertical) // 입력받은 값에따라 애니메이션이 블랜드 되는데 너무 확확 바껴서 가속으로  
    {                                                         // 바꾸려고 함..
        m_animator.SetFloat("Horizontal", horizontal);
        m_animator.SetFloat("Vertical", vertical);
    }
}
