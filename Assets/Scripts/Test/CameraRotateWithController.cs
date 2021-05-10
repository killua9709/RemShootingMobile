using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateWithController : MonoBehaviour
{
    [SerializeField] Transform m_target;
    [SerializeField] float m_distance;

    float m_pitch = 0f;
    float m_yaw = 0f;


    public void AddPitchAndYaw(float pitch, float yaw)
    {
        m_pitch += pitch;
        m_pitch = Mathf.Clamp(m_pitch, -89f, 89f);//위아래 범위제한 lookat함수가 외적으로 나오는 거라서 90도가 되면 010으로 된대
        m_yaw += yaw;
        UpdateCameraByProperty();
    }

    public void UpdateCameraByProperty()
    {
        Quaternion rotation = Quaternion.Euler(m_pitch, m_yaw, 0f);
        transform.position = m_target.position + rotation * new Vector3(0f, 0f, -m_distance);
        transform.LookAt(m_target.position);
    }


    //public void AddPitch(float value)
    //{
    //    m_pitch += value;
    //    UpdateCameraByProperty();
    //}
    //
    //public void AddYaw(float value)
    //{
    //    m_yaw += value;
    //    UpdateCameraByProperty();
    //}
}
