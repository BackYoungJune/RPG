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

    public Vector3 Target;

    void Awake()
    {
        myAnim = GetComponent<Animator>();
        myNavAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        StateProcess();
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(Input.mousePosition);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 999.0f, ClickMask))
            {
                Target = hit.point;
                ChangeState(STATE.NORMAL);
                Debug.Log("Target : " + Target);
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
                    myNavAgent.stoppingDistance = 0.5f;

                    break;
                }
            case STATE.MOVE:
                {
                    break;
                }
            case STATE.BATTLE:
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
                    myAnim.SetFloat("Speed", myNavAgent.velocity.magnitude / myNavAgent.speed);
                    myNavAgent.SetDestination(Target);
                    break;
                }
        }
    }

}
