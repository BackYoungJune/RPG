using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public enum STATE
    {
        NORMAL, ATTACK, SKILL
    }
    public STATE myState = STATE.NORMAL;

    public Animator myAnim;
    public PlayerAnimEvent myAnimEvent;
    public LayerMask ClickMask;
    public float AttackTime;

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myAnimEvent = GetComponent<PlayerAnimEvent>();
    }

    private void Start()
    {
        myAnimEvent.Attack += OnAttack;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, 999.0f, ClickMask))
            //{
            //    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            //    {
            //        Target = hit.point;
            //        ChangeState(STATE.BATTLE);
            //    }
            //    else
            //    {
            //        Target = hit.point;
            //        ChangeState(STATE.NORMAL);
            //    }
            //}
        }

        StateProcess();
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
                    if (Input.GetMouseButtonDown(0))
                    {
                        ChangeState(STATE.ATTACK);
                    }

                    break;
                }
            case STATE.ATTACK:
                {
                    //if (Target != Vector3.zero)
                    //{
                    //    Vector3 dir = Target - transform.position;
                    //    //dir.y = 0;  // 평면상으로만 이동하려고 y = 0 했다
                    //    dir.Normalize();
                    //    transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), Time.smoothDeltaTime * 4.0f);
                    //}

                    //myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    //myNavAgent.SetDestination(Target);
                    break;
                }
            case STATE.SKILL:
                {
                    break;
                }
        }
    }

    public void OnAttack()
    {

    }
}
