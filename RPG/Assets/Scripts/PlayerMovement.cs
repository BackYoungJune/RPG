﻿using System.Collections;
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

    void Awake()
    {
        myAnim = GetComponent<Animator>();
        myNavAgent = GetComponent<NavMeshAgent>();
        myAnimEvent = GetComponent<PlayerAnimEvent>();
    }

    void Update()
    {
        StateProcess();
        if (Input.GetMouseButtonDown(1))
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
        }
    }
}
