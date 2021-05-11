using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public Transform playerBody; //카메라가 돌때 돌릴 플레이어의 몸의 변화를 위한 변수
    public Transform m_viewPoint;//뷰포인트
    float m_xRotate = 0f;
    float m_yRotate = 0f;
    //}}

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;//마우스를 잠근다
    }

    void Update()
    {
        //}}2 각도를 구하고 좌표를 바꿔주는 형식으로 만드려고 함
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");
        float distance = 2f;
        float pitch = -mouseY;

        //playerBody.Rotate(Vector3.up * mouseX);//마우스x의 값에 따라 플레이어 회전

        m_xRotate += pitch * 180f * Time.deltaTime; 
        m_xRotate = Mathf.Clamp(m_xRotate, -90f, 90f); // 각도 고정

        Quaternion rotation = Quaternion.Euler(m_xRotate, 0f, 0f);
        transform.rotation = rotation;
        
        Vector3 dir = transform.forward;

        transform.position = m_viewPoint.position - dir * distance;
        
        //}}



        //{{1첫시도

        //transform.RotateAround(m_viewPoint.position, Vector3.up, 360f * Time.deltaTime);

        //float mouseX = Input.GetAxis("Mouse X") * m_speed * Time.deltaTime;
        //float mouseY = Input.GetAxis("Mouse Y") * m_speed * Time.deltaTime;
        //
        //xRotation -= mouseY;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        //
        //transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        //playerBody.Rotate(Vector3.up * mouseX);
        //
        ////Vector3 vector = transform.position;
        ////vector.y = transform.position.y * Mathf.Sin(xRotation);
        ////vector.z = transform.position.z * Mathf.Cos(xRotation);
        ////transform.position = vector;
        //
        //Vector3 dir = new Vector3(mouseX, 0f, mouseY);

        //}}
    }
}
