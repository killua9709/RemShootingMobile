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
        UpdateRotate();
        UpdateTranslate();
        m_cameraRotate.UpdateCameraByProperty();
    }

    void UpdateRotate()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        float mouseX = Input.GetAxis("Mouse X");

        m_cameraRotate.AddPitchAndYaw(mouseY * 180f * Time.deltaTime,
            mouseX * 180f * Time.deltaTime);

        m_character.SetYawAngle(m_cameraRotate.transform.eulerAngles.y);
    }

    void UpdateTranslate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Vector3 dir = new Vector3(horizontal, 0, vertical); //움직이는 것이 y축이 아니라 x,z축만을 이동한다.
        dir.Normalize();//정규화


    }
}
