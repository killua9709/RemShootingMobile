using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]CameraRotateWithController m_cameraRotate;
    [SerializeField] CharacterWithController m_character;

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
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(horizontal, 0, vertical); //움직이는 것이 y축이 아니라 x,z축만을 이동한다.

        if(dir != Vector3.zero)
        {
            //dir.Normalize();//정규화
            //dir = Camera.main.transform.TransformDirection(dir); //내가 바라보는 방향으로 (카메라의 방향으로)이동하고 싶다
            //dir.y = 0;
            
            m_character.Move(dir.normalized);
        }
        else
        {
            m_character.MoveEnd();
        }

        

        //if (dir.y == 0)
        //{
        //    if (horizontal == 0)
        //    {
        //        if (vertical < 0)
        //            m_character.SetState(1);
        //        else if (vertical > 0)
        //            m_character.SetState(2);
        //        else m_character.SetState(0);
        //    }
        //    else if (vertical == 0)
        //    {
        //        if (horizontal < 0)
        //            m_character.SetState(3);
        //        else if (horizontal > 0)
        //            m_character.SetState(4);
        //    }
        //    else if (horizontal != 0 && vertical != 0)
        //    {
        //        if (vertical < 0)
        //            m_character.SetState(1);
        //        else if (vertical > 0)
        //            m_character.SetState(2);
        //    }
        //}
        //
        //if ( cc.collisionFlags== CollisionFlags.Below) //만약 player가 바닥에 닿는다면
        //{
        //    jumpCount = 0; // 점프카운트를 0으로
        //    yVelocity = 0; //중력을 0으로   
        //}
        //
        //if (Input.GetButtonDown("Jump")) // 만약 점프키(space bar)누르면
        //{
        //    if (jumpCount == 0 && cc.collisionFlags != CollisionFlags.Below) //만약 점프키카운트가 0인 동시에 player가 바닥에 닿지 않는다면
        //    {
        //        return;
        //    }
        //    else if (jumpCount < maxJumpCount) //그렇지 않고 만약 점프카운트가 최대횟수보다 작다면
        //    {
        //        m_character.SetState(5);
        //    }
        //}   
        //
        //yVelocity += gravity * Time.deltaTime; // v=v0+at
        //dir.y = yVelocity;//y방향이 없었는데, y에 중력을 적용함
        //                  // transform.position += dir * playerMovespeed * Time.deltaTime;//p=p0*vt
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
