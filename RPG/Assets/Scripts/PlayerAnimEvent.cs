using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidDelVoid();

public class PlayerAnimEvent : MonoBehaviour
{
    public VoidDelVoid Attack = null;       // 플레이어 공격 적중 이벤트
    public VoidDelVoid AttackEnd = null;    // 플레이어 공격이 끝난 경우 이벤트 

    void OnAttack()
    {
        Attack?.Invoke(); 
    }

    void EndAttack()
    {
        AttackEnd?.Invoke();
    }
}
