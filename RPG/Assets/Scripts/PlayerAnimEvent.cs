using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void VoidDelVoid();

public class PlayerAnimEvent : MonoBehaviour
{
    public VoidDelVoid Attack = null;

    void OnAttack()
    {
        Attack?.Invoke(); 
    }
}
