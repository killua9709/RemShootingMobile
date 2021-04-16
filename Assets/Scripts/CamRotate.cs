using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public float m_speed = 100f; //카메라를 돌릴때의 속도
    public Transform playerBody; //카메라가 돌때 돌릴 플레이어의 몸의 변화를 위한 변수
    float xRotation = 0f; //
    //float mX;
    //float mY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//마우스를 잠근다
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * m_speed * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * m_speed * Time.deltaTime;
        
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);

        Vector3 dir = new Vector3(mouseX, 0f, mouseY);
    }
}
