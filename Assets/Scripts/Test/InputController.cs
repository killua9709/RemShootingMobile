using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField]CameraRotateWithController m_cameraRotate;
    [SerializeField] CharacterWithController m_character;
    public float m_speed;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//마우스를 잠근다
        UpdateRotate();
    }

    void Update()
    {
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

        dir.Normalize();//정규화

        if (vertical == 0)
            m_character.SetState(0);
        else if (vertical < 0)
            m_character.SetState(1);
        else if (vertical > 0)
            m_character.SetState(2);

        dir = Camera.main.transform.TransformDirection(dir); //내가 바라보는 방향으로 (카메라의 방향으로)이동하고 싶다.

        dir.y = 0;

        m_character.GetComponent<CharacterController>().Move(dir * m_speed * Time.deltaTime);

    }
}
