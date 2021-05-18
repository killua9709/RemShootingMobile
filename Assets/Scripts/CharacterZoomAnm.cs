using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterZoomAnm : MonoBehaviour
{
    public Transform target;// 바라볼 타겟
    public Vector3 relativeVec; //물체에 대한 상대적인 벡터

    private Animator anim; // 애니메이션
    private Transform spine; // 아바타의 상체
    bool aimMode = false;

    private void Start()
    {
        anim = GetComponent<Animator>();
        spine = anim.GetBoneTransform(HumanBodyBones.Spine); // 상체값 가져오기 (허리 위)
    }

    private void LateUpdate()
    {
        if(Input.GetMouseButtonDown(1)) // 우클릭 눌리는 동안
        {
            Debug.Log("바라보기");
            StartCoroutine(aimModeOn());
        }
        else if(Input.GetMouseButtonUp(1)) // 우킬릭을 때면
        {
            Debug.Log("해제");
            StartCoroutine(aimModeOff());
        }
        if(aimMode == true) //에임모드가 활성화 되면
        {
            spine.LookAt(target.position); // 플레이어의 상체부분이 타겟 위치 보기
            spine.rotation = spine.rotation * Quaternion.Euler(relativeVec); // 타겟으로 회전
        }
    }

    IEnumerator aimModeOn() // 에임모드가 켜지면 지정대기시간 이후 aimmode = true
    {
        Debug.Log("조준");
        yield return new WaitForSeconds(0.07f);
        aimMode = true;
    }

    IEnumerator aimModeOff() // 에임모드가 꺼짐녀 지정대기시간이후 aimMode = false
    {
        yield return new WaitForSeconds(0.01f);
        aimMode = false;
    }
}
