using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] CameraRotateWithController m_cameraRotate;
    [SerializeField] CharacterWithController m_character;
    [SerializeField] GunRotate m_gun;
    float yVelocity = 0;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//마우스를 잠근다
        UpdateRotate();
    }

    void Update()
    {
        UpdateRotate();
        UpdateTranslate();
        //UpdateViewpoint();
        m_cameraRotate.UpdateCameraByProperty();

        if(Input.GetMouseButtonDown(1))
        {
            m_cameraRotate.StartAiming(0.3f);
        }
        else if(Input.GetMouseButtonUp(1))
        {
            m_cameraRotate.ToDefault(0.3f);
        }
       
    }
    private void LateUpdate()
    {
        UpdateViewpoint();
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
        
        yVelocity += m_character.m_gravity * Time.deltaTime;

        if (dir != Vector3.zero)
        {
            m_character.Move(dir.normalized);
        }
        else
        {
            m_character.MoveEnd();
        }

        if (m_character.m_characterController.collisionFlags == CollisionFlags.Below) //만약 player가 바닥에 닿는다면
        {
            m_character.m_jumpcount = 0; // 점프카운트를 0으로
            yVelocity = 0; 
        }

        if (Input.GetButtonDown("Jump") && m_character.m_jumpcount<2)    //점프키를 누르고 점프카운트가 있다면
        {
            yVelocity = m_character.m_jumpPower;
            m_character.m_jumpcount++;
        }
        
        m_character.m_characterController.Move(new Vector3(0,yVelocity,0));

        if (vertical < 0)
        {
            GameObject.Find("ReichsrevolverM1879").transform.Rotate(new Vector3(1000f, 0, 0) * Time.deltaTime);
        }
        else GameObject.Find("ReichsrevolverM1879").transform.localRotation = Quaternion.Euler(0, 90, 90);
    }

    void UpdateViewpoint()
    {
        //if(Input.GetMouseButton(1))
        //{
        //    m_cameraRotate.MoveViewpoint();
        //}
        //
        //if(Input.GetMouseButtonUp(1))
        //{
        //    m_cameraRotate.ReturnViewpoint();
        //}
    }
}
