using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForSeop : MonoBehaviour
{
    [SerializeField] Transform m_target;

    [SerializeField]float m_distance = 10f;
    float m_pitch = 0f;
    float m_yaw = 0f;
    [SerializeField] float m_sensitiveX = 4f;
    [SerializeField] float m_sensitiveY = 1f;
    [SerializeField] Transform playerBody;

    private void Update()
    {
        m_yaw += m_sensitiveX * Input.GetAxis("Mouse X");
        m_pitch -= m_sensitiveY * Input.GetAxis("Mouse Y");

        m_pitch = Mathf.Clamp(m_pitch, 90f, -90f);

        Quaternion rotation = Quaternion.Euler(m_pitch, m_yaw, 0f);
        transform.position = m_target.position + rotation * new Vector3(0f, 0f, -m_distance);
        transform.LookAt(m_target.position);
    }

    //void LateUpdate()
    //{
    //    playerBody.Rotate(Vector3.up * m_yaw);
    //}
}