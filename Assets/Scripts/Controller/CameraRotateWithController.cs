using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotateWithController : MonoBehaviour
// 카메라의 위치지정과 함수들
{
    enum State
    {
        Default,
        ToAniming,
        Animing,
        ToDefault
    }

    [SerializeField] Transform m_target;    //어떤 곳을 기점으로 위치를 생성할지 위해 
    [SerializeField] Transform m_target2;   //이동시킬 곳
    [SerializeField] Transform m_Inittarget;//원래위치
    [SerializeField] Transform m_lookPoint;
    [SerializeField] float m_distance;      //얼마만큼 떨어질지
    float m_Initdistance;
    bool pointmove = false;

    float m_currentTime;
    float m_durationTime;
    Vector3 m_startPosition;

    float m_pitch = 0f;                     //종축
    float m_yaw = 0f;                       //수직축

    [SerializeField]State m_state = State.Default;

    void Awake()
    {
        m_Initdistance = m_distance;
    }

    public void AddPitchAndYaw(float pitch, float yaw)
    {
        if (m_state == State.Default)
        {
            m_pitch += pitch;                   //종축(pitch)을 증가시킨다
            m_pitch = Mathf.Clamp(m_pitch, -45f, 60f);//위아래 범위제한 lookat함수가 외적으로 나오는 거라서 90도가 되면 010으로 된대
            m_yaw += yaw;                       //수직축(yaw)을 증가시킨다.
            UpdateCameraByProperty();           //
        }
        else if(m_state ==State.ToAniming)
        {
            m_currentTime += Time.deltaTime;
            float ratio = m_currentTime / m_durationTime;
            transform.position = Vector3.Lerp(m_startPosition, m_lookPoint.position, ratio);

            if(ratio >= 1f)
            {
                transform.position = m_lookPoint.position;
                m_state = State.Animing;
            }
        }
        else if(m_state ==State.ToDefault)
        {
            m_currentTime += Time.deltaTime;
            float ratio = m_currentTime / m_durationTime;
            Vector3 targetPoint = GetCameraPosition();
            transform.position = Vector3.Lerp(m_startPosition, targetPoint, ratio);

            if (ratio >= 1f)
            {
                transform.position = targetPoint;
                m_state = State.Default;
            }
        }
    }

    public void UpdateCameraByProperty()
    {
        if (m_state == State.Default)
        {

            transform.position = GetCameraPosition();
            transform.LookAt(m_target.position);                                                  //포지션 이동 후 바라보는 회전을 타겟의 포지션에 맞춘다.
        }
    }

    public void StartAiming(float durationTime)
    {
        m_state = State.ToAniming;
        m_currentTime = 0f;
        m_durationTime = durationTime;
        m_startPosition = transform.position;
    }

    public void ToDefault(float durationTime)
    {
        m_state = State.ToDefault;
        m_startPosition = transform.position;
        m_currentTime = 0f;
        m_durationTime = durationTime;
    }

    Vector3 GetCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(m_pitch, m_yaw, 0f); //입력받은 값들에 대해 회전을 대입

        return m_target.position + rotation * new Vector3(0f, 0f, -m_distance); //쿼터니언과 벡터를 곱하면 회전한 방향벡터를 얻을 수 있다.
    }

    //public void MoveViewpoint()
    //{
    //    Vector3 dir = m_target2.localPosition - m_target.localPosition;
    //    if (dir.magnitude < 0.1f)
    //    {
    //        m_distance = 0.5f;
    //        pointmove = true;
    //    }
    //    else
    //    {
    //        dir.Normalize();
    //        m_target.Translate(dir * 2.5f * Time.deltaTime);
    //        m_distance -= 2.5f * Time.deltaTime;
    //    }
    //}
    //
    //public void ReturnViewpoint()
    //{
    //    pointmove = false;
    //    m_target.position = m_Inittarget.position;
    //    m_distance = m_Initdistance;
    //}
}
