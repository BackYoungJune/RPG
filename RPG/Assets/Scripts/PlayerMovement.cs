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
    public STATE myState = STATE.NORMAL;

    public LayerMask ClickMask;
    public NavMeshAgent myNavAgent;
    public Animator myAnim;
    public PlayerAnimEvent myAnimEvent;

    public Vector3 Target;

    public float AttackTime;

    void Awake()
    {
        myAnim = GetComponent<Animator>();
        myNavAgent = GetComponent<NavMeshAgent>();
        myAnimEvent = GetComponent<PlayerAnimEvent>();
    }

    private void Start()
    {
        myAnimEvent.Attack += OnAttack;
    }

    void Update()
    {
        StateProcess();
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 999.0f, ClickMask))
            {
                if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    Target = hit.point;
                    ChangeState(STATE.BATTLE);
                }
                else
                {
                    Target = hit.point;
                    ChangeState(STATE.NORMAL);
                }
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
                    myNavAgent.stoppingDistance = 2.0f;

                    break;
                }
            case STATE.MOVE:
                {
                    break;
                }
            case STATE.BATTLE:
                {
                    AttackTime = 3.0f;
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
                    if (Target != Vector3.zero)
                    {
                        Vector3 dir = Target - transform.position;
                        //dir.y = 0;  // 평면상으로만 이동하려고 y = 0 했다
                        dir.Normalize();
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 4.0f);
                    }

                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    myNavAgent.SetDestination(Target);
                    break;
                }
            case STATE.MOVE:
                {
                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    myNavAgent.SetDestination(Target);
                    break;
                }
            case STATE.BATTLE:
                {
                    if (Target != Vector3.zero)
                    {
                        Vector3 dir = Target - transform.position;
                        //dir.y = 0;  // 평면상으로만 이동하려고 y = 0 했다
                        dir.Normalize();
                        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 4.0f);
                    }

                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    myNavAgent.SetDestination(Target);

                    if(AttackTime > 3.0f)
                    {
                        myAnim.SetTrigger("Attack");
                        AttackTime = 0.0f;
                    }
                    else
                    {
                        AttackTime += Time.deltaTime;
                    }
                    break;
                }
        }
    }

    void OnAttack()
    {
        float distance = Vector3.Distance(transform.position, Target);
        Vector3 dir = transform.position - Target;
        float dot = Vector3.Dot(dir, transform.forward);
        if(dot > 0 && distance < 3.0f)
        {
            Debug.Log("damage");
        }
    }

}
