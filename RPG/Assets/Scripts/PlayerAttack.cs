using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public enum STATE
    {
        NORMAL, ATTACK, SKILL
    }
    public STATE myState = STATE.NORMAL;    // 현재 상태를 나타내는 STATE

    public Animator myAnim;                 // 플레이어 애니메이션
    public PlayerAnimEvent myAnimEvent;     // 플레이어 애니메이션 이벤트
    public PlayerInput playerInput;         // 플레이어 입력 감지 컴포넌트

    public LayerMask layerMask;             // 공격 타격 받을 Layer
    public float AttackTime;                // 공격 시간
    public int damage;                      // 공격 데미지
    public float Attackrange;               // 공격 범위

    

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myAnimEvent = GetComponent<PlayerAnimEvent>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        myAnimEvent.Attack += OnAttack;
        myAnimEvent.AttackEnd += EndAttack;
    }

    private void FixedUpdate()
    {
        StateProcess();
        //OnDrawGizmos();
    }

    public void OnAttack()
    {
        // Player와 Enemy의 거리를 구함
        //float distance = Vector3.Distance(myRangeSys.Target.position, transform.position);
        //Vector3 dir = myRangeSys.Target.position - transform.position;
        //Vector3.Dot(dir, transform.forward);
        //// Player가 공격을 받을 시 일정 거리 이상 멀어지면 공격을 받지 않는다
        //// 거리가 1.2f이하, 정면에 상대가 있을 경우
        //if (distance < 1.2f && Vector3.Dot(dir, transform.forward) > 0)
        //    myRangeSys.Target.GetComponent<LivingEntity>()?.OnDamage(damage, hitPoint, hitNormal);
    }

    public void EndAttack()
    {
        ChangeState(STATE.NORMAL);
    }

    void ChangeState(STATE s)
    {
        if (myState == s) return;
        myState = s;

        switch (myState)
        {
            case STATE.NORMAL:
                {
                    break;
                }
            case STATE.ATTACK:
                {
                    myAnim.SetTrigger("Attack");
                    damage = 20;
                    Attackrange = 10.0f;
                    break;
                }
            case STATE.SKILL:
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
                    if (playerInput.MouseLeftButton)
                    {
                        ChangeState(STATE.ATTACK);
                    }

                    break;
                }
            case STATE.ATTACK:
                {
                    

                    //// Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
                    //RaycastHit hit;
                    //Debug.DrawRay(transform.position, transform.forward * Attackrange, Color.blue, 0.3f);

                    //if (Physics.Raycast(transform.position, transform.forward, out hit, Attackrange, layerMask))
                    //{
                    //    Debug.Log("1");
                    //    if (hit.transform.tag == "Enemy")
                    //    {
                    //        Debug.Log("Attack");
                    //    }
                    //}
                    break;
                }
            case STATE.SKILL:
                {
                    break;
                }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;

        // 함수 파라미터 : 현재 위치, Box의 절반 사이즈, Ray의 방향, RaycastHit 결과, Box의 회전값, BoxCast를 진행할 거리
        if (true == Physics.BoxCast(transform.position, transform.lossyScale / 2.0f, transform.forward, out RaycastHit hit, transform.rotation, Attackrange))
        {
            // Hit된 지점까지 ray를 그려준다.
            Debug.DrawRay(transform.position, transform.forward * hit.distance);

            // Hit된 지점에 박스를 그려준다.
            Gizmos.DrawWireCube(transform.position + transform.forward * hit.distance, transform.lossyScale);
        }
        else
        {
            // Hit가 되지 않았으면 최대 검출 거리로 ray를 그려준다.
            Gizmos.DrawRay(transform.position, transform.forward * Attackrange);
        }
    }

    

}
