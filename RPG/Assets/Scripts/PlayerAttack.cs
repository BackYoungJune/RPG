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
    public PlayerInput playerInput;

    public float AttackTime;
    public int damage;
    public float Attackrange;

    

    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        myAnimEvent = GetComponent<PlayerAnimEvent>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Start()
    {
        myAnimEvent.Attack += OnAttack;
    }

    private void Update()
    {
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
                    RaycastHit hit;
                    Debug.DrawRay(transform.position, transform.forward * Attackrange, Color.blue, 0.3f);
                    if (Physics.Raycast(transform.position, transform.forward, out hit, Attackrange))
                    {
                        Debug.Log("1");
                        if (hit.transform.tag == "Enemy")
                        {
                            Debug.Log("Attack");
                        }
                    }
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
        ChangeState(STATE.NORMAL);
    }
}
