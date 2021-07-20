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
    public float Attackradius;              // 공격 반경


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
    }

    public void OnAttack()
    {
        RaycastHit[] rayHits = Physics.SphereCastAll(transform.position, Attackradius, transform.forward, Attackrange);
        foreach (RaycastHit hit in rayHits)
        {
            if(hit.transform.gameObject.tag == "Enemy")
            {
                Debug.Log("Enemy");
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                Vector3 dir = transform.position - hit.transform.position;
                float dot = Vector3.Dot(dir, transform.forward);
                if (distance < 11.0f && dot >= 0)
                {
                    Debug.Log("distance");
                    hit.transform.GetComponent<LivingEntity>()?.OnDamage(damage, hit.point, hit.normal);
                }
            }
        }
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
                    Attackrange = 7.0f;
                    Attackradius = 9.0f;
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


                    break;
                }
            case STATE.SKILL:
                {
                    break;
                }
        }
    }

    void OnDrawGizmos()
    {

        //float maxDistance = 10;
        RaycastHit hit;
    // Physics.SphereCast (레이저를 발사할 위치, 구의 반경, 발사 방향, 충돌 결과, 최대 거리)
    bool isHit = Physics.SphereCast(transform.position, Attackradius, transform.forward, out hit, Attackrange);

    Gizmos.color = Color.red;
        if (isHit)
        {
            Gizmos.DrawRay(transform.position, transform.forward* hit.distance);
            Gizmos.DrawWireSphere(transform.position + transform.forward* hit.distance, Attackradius );
}
        else
{
    Gizmos.DrawRay(transform.position, transform.forward * Attackrange);
}

    }
}