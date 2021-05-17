using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateWithController : MonoBehaviour
// 카메라의 위치지정과 함수들
{
    [SerializeField] Transform m_target;    //어떤 곳을 기점으로 위치를 생성할지 위해 
    [SerializeField] Transform m_target2;   //이동시킬 곳
    [SerializeField] Transform m_Inittarget;//원래위치
    [SerializeField] float m_distance;      //얼마만큼 떨어질지
    float m_Initdistance;

    float m_pitch = 0f;                     //종축
    float m_yaw = 0f;                       //수직축

    void Awake()
    {
        m_Initdistance = m_distance;
    }

    public void AddPitchAndYaw(float pitch, float yaw)
    {
        m_pitch += pitch;                   //종축(pitch)을 증가시킨다
        m_pitch = Mathf.Clamp(m_pitch, -45f, 60f);//위아래 범위제한 lookat함수가 외적으로 나오는 거라서 90도가 되면 010으로 된대
        m_yaw += yaw;                       //수직축(yaw)을 증가시킨다.
        UpdateCameraByProperty();           //
    }

    public void UpdateCameraByProperty()
    {
        Quaternion rotation = Quaternion.Euler(m_pitch, m_yaw, 0f);
        transform.position = m_target.position + rotation * new Vector3(0f, 0f, -m_distance); //쿼터니언과 벡터를 곱하면 회전한 방향벡터를 얻을 수 있다.
        transform.LookAt(m_target.position);                                                  //포지션 이동 후 바라보는 회전을 타겟의 포지션에 맞춘다.

        m_target2.position = m_target.position + rotation * new Vector3(0f, 0f, +m_distance*2);
    }

    public void MoveViewpoint()
    {
        Vector3 dir = m_target2.localPosition - m_target.localPosition;
        if (dir.magnitude < 0.1f) return;
        dir.Normalize();
        m_target.Translate(dir * 2.5f * Time.deltaTime);
        m_distance -= 2.5f*Time.deltaTime;
        if (m_distance < 0.1f) m_distance = 0.1f;

    }

    public void ReturnViewpoint()
    {
        m_target.position = m_Inittarget.position;
        m_distance = m_Initdistance;
    }
}
