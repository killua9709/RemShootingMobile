using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]CameraRotateWithController m_cameraRotate;
    [SerializeField] CharacterWithController m_character;
    public float m_speed;
    public float gravity = -20; // 중력가속도 원래는 9.8인데 크기 20으로 일단 설정
    float yVelocity; //얘가 중력

    public float jumpPower = 5; // 점프 파워를 5로 설정, unity에서 수정가능
    public int maxJumpCount = 2; //2단 점프이므로 점프 제한 횟수를 2회로설정
    int jumpCount = 0; //현재 점프 횟수

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//마우스를 잠근다
        UpdateRotate();
    }

    void Update()
    {
        UpdateViewpoint();
        UpdateRotate();
        UpdateTranslate();
        m_cameraRotate.UpdateCameraByProperty();
       
    }

    void UpdateRotate() // 카메라와 플레이어에 입력값에 따른 회전값
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        m_cameraRotate.AddPitchAndYaw(mouseY * 180f * Time.deltaTime, mouseX * 180f * Time.deltaTime);

        m_character.SetYawAngle(m_cameraRotate.transform.eulerAngles.y);
    }

    void UpdateTranslate() // 플레이어에 입력값에 따른 이동 값
    {
        CharacterController cc = m_character.GetComponent<CharacterController>();
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(horizontal, 0, vertical); //움직이는 것이 y축이 아니라 x,z축만을 이동한다.

        dir.Normalize();//정규화

        if (horizontal == 0)
        {
            if (vertical < 0)
                m_character.SetState(1);
            else if (vertical > 0)
                m_character.SetState(2);
            else m_character.SetState(0);
        }
        else if (vertical == 0)
        {
            if (horizontal < 0)
                m_character.SetState(3);
            else if (horizontal > 0)
                m_character.SetState(4);
        }
        else if (horizontal != 0 && vertical != 0)
        {
            if (vertical < 0)
                m_character.SetState(1);
            else if (vertical > 0)
                m_character.SetState(2);
        }

        dir = Camera.main.transform.TransformDirection(dir); //내가 바라보는 방향으로 (카메라의 방향으로)이동하고 싶다
        dir.y = 0;

        if ( cc.collisionFlags== CollisionFlags.Below) //만약 player가 바닥에 닿는다면
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

        yVelocity += gravity * Time.deltaTime; // v=v0+at
        dir.y = yVelocity;//y방향이 없었는데, y에 중력을 적용함
                          // transform.position += dir * playerMovespeed * Time.deltaTime;//p=p0*vt

        m_character.GetComponent<CharacterController>().Move(dir * m_speed * Time.deltaTime);
    }

    void UpdateViewpoint()
    {
        if(Input.GetMouseButton(1))
        {
            m_cameraRotate.MoveViewpoint();
        }

        if(Input.GetMouseButtonUp(1))
        {
            m_cameraRotate.ReturnViewpoint();
        }
    }
}
