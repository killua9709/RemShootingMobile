using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWithController : MonoBehaviour
{
    public float playerMovespeed = 5;
    public float gravity = -20; // 중력가속도 원래는 9.8인데 크기 20으로 일단 설정
    float yVelocity; //얘가 중력

    public float jumpPower = 5; // 점프 파워를 5로 설정, unity에서 수정가능
    public int maxJumpCount = 2; //2단 점프이므로 점프 제한 횟수를 2회로설정
    int jumpCount = 0; //현재 점프 횟수

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
        Back = 2
    }

    CharacterController cc;//character controller이용
    void Start()
    {
        m_animator = GetComponent<Animator>();
        cc = gameObject.GetComponent<CharacterController>(); //나의 컴포넌트 중에 character controller를 가져온다
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LateUpdate()
    {
        //float h = Input.GetAxis("Horizontal"); //사용자 입력처리
        //float v = Input.GetAxis("Vertical");
        //
        //
        //Vector3 dir = new Vector3(h, 0, v); //움직이는 것이 y축이 아니라 x,z축만을 이동한다.
        //dir.Normalize();//정규화

        //dir = Camera.main.transform.TransformDirection(dir); //내가 바라보는 방향으로 (카메라의 방향으로)이동하고 싶다.

        //-90에서 90까지의 y축좌표기준 회전일때는 정상적인데 그 반대일때는 거꾸로 애니메이션이 작동한다.
        //그래서 y좌표기준이 반대일때를 지정해서 넣었더니 그래도 안 된다... 왜 그럴까..

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
        }

        if (cc.collisionFlags == CollisionFlags.Below) //만약 player가 바닥에 닿는다면
        {
            jumpCount = 0; // 점프카운트를 0으로
            yVelocity = 0; //중력을 0으로
        }

        if (Input.GetButtonDown("Jump")) // 만약 점프키(space bar)누르면
        {

            if (jumpCount == 0 && cc.collisionFlags != CollisionFlags.Below) //만약 점프키카운트가 0인 동시에 player가 바닥에 닿지 않는다면
            {
                return;
            }
            else if (jumpCount < maxJumpCount) //그렇지 않고 만약 점프카운트가 최대횟수보다 작다면
            {
                yVelocity = jumpPower;//중력 = 점프파워
                jumpCount++; // 점프카운트를 1증가
            }
        }

        //yVelocity += gravity * Time.deltaTime; // v=v0+at
        //dir.y = yVelocity;//y방향이 없었는데, y에 중력을 적용함
        //                  // transform.position += dir * playerMovespeed * Time.deltaTime;//p=p0*vt
        //
        //cc.Move(dir * playerMovespeed * Time.deltaTime);
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
