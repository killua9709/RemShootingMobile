using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithController : MonoBehaviour
{
    readonly int c_animHashKeyState = Animator.StringToHash("State");
    readonly int c_animHashKeyDie = Animator.StringToHash("Die");
    [SerializeField] State m_state = State.Idle;
    Animator m_animator;

    int m_statenum = 0;

    //[Header(("State"))

    enum State : int
    {
        Idle = 0,
        Run = 1,
        Back = 2,
        Left = 3,
        Right = 4,
        LeftRun = 5,
        RightRun = 6
    }

    CharacterController cc;//character controller이용
    void Start()
    {
        m_animator = GetComponent<Animator>();
        cc = gameObject.GetComponent<CharacterController>(); //나의 컴포넌트 중에 character controller를 가져온다
    }

    void Update()
    {
        switch(m_statenum)
        {
            case 0:
                ChangeState(State.Idle);
                break;
            case 1:
                ChangeState(State.Back);
                break;
            case 2:
                ChangeState(State.Run);
                break;
            case 3:
                ChangeState(State.Left);
                break;
            case 4:
                ChangeState(State.Right);
                break;
        }
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

    public void Move(Vector3 velocity)
    {
        transform.Translate(velocity, Space.World);
    }

    public void SetState(int statenum)
    {
        m_statenum = statenum;
    }
}
