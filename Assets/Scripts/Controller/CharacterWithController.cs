using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithController : MonoBehaviour
{
    readonly int c_animHashKeyState = Animator.StringToHash("State");
    readonly int c_animHashKeyDie = Animator.StringToHash("Die");
    [SerializeField] State m_state = State.Idle;
    Animator m_animator;

    [SerializeField]float m_speed = 5f;

    //[Header(("State"))

    enum State : int
    {
        Idle = 0,
        Run = 1,
        Back = 2,
        Left = 3,
        Right = 4,
        Jump = 5
    }

    CharacterController m_characterController;//character controller이용
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_characterController = GetComponent<CharacterController>(); //나의 컴포넌트 중에 character controller를 가져온다
    }

    void Update()
    {
        
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
        Debug.Log("MoveDir : " + localDir);
        //
        //m_character.GetComponent<CharacterController>().Move(dir * m_speed * Time.deltaTime);
    }

    public void MoveEnd()
    {
        ChangeState(State.Idle);
        SetRunAnimProperty(0f, 0f);
    }

    void SetRunAnimProperty(float horizontal, float vertical)
    {
        m_animator.SetFloat("Horizontal", horizontal);
        m_animator.SetFloat("Vertical", vertical);
    }
}
