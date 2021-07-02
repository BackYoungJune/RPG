using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    public enum STATE
    {
        NORMAL, MOVE, BATTLE
    }
    public STATE myState = STATE.NORMAL;    // 현재 상태를 나타내는 STATE

    public LayerMask ClickMask;             // 클릭하는 지점의 Layer
    public NavMeshAgent myNavAgent;         // 플레이어 Navigation
    public Animator myAnim;                 // 플레이어 애니메이션
    public PlayerAnimEvent myAnimEvent;     // 플레이어 애님 이벤트
    public PlayerInput playerInput;         // 플레이어 입력 감지 컴포넌트

    public Vector3 Target;                  // 마우스 클릭지점 Vector3

    void Awake()
    {
        // 사용할 컴포넌트들을 받아온다
        myAnim = GetComponent<Animator>();
        myNavAgent = GetComponent<NavMeshAgent>();
        myAnimEvent = GetComponent<PlayerAnimEvent>();
        playerInput = GetComponent<PlayerInput>();
    }

    void FixedUpdate()
    {
        StateProcess();
        // 마우스 우클릭을 했을경우
        if (playerInput.MouseRightButton)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 999.0f, ClickMask))
            {
                Target = hit.point;
                ChangeState(STATE.NORMAL);
            }
        }
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch(myState)
        {
            case STATE.NORMAL:
                {
                    // 플레이어 간격 제한
                    myNavAgent.stoppingDistance = 2.0f;

                    break;
                }
            case STATE.MOVE:
                {
                    break;
                }
        }
    }

    void StateProcess()
    {
        switch (myState)
        {
            case STATE.NORMAL:
                {
                    // 이동할 위치가 정해진경우 이동시킨다
                    if (Target != Vector3.zero)
                    {
                        Vector3 dir = Target - transform.position;
                        //dir.y = 0;  // 평면상으로만 이동하려고 y = 0 했다
                        dir.Normalize();
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 4.0f);
                    }

                    // 애니메이션 속도 셋팅
                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    myNavAgent.SetDestination(Target);
                    break;
                }
            case STATE.MOVE:
                {
                    // 애니메이션 속도 셋팅
                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    myNavAgent.SetDestination(Target);
                    break;
                }
        }
    }
}
