using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour, damageable
{
    public float startHealth = 200.0f;              // 시작 체력
    public float health { get; protected set; }     // 현재 체력

    public float startMana = 200.0f;                // 시작 마나
    public float mana { get; protected set; }       // 현재 마나

    public bool dead { get; protected set; }        // 사망 상태
    public event Action onDeath;                    // 사망 시 발동할 이벤트

    protected virtual void OnEnable()
    {
        dead = false;            // 살아있는 상태
        health = startHealth;    // 체력 초기화
        mana = startMana;        // 마나 초기화
    }

    public virtual void OnDamage(float damage, Vector3 hitPoint, Vector3 hitNormal)
    {
        // 데미지를 입힌다
        health -= damage;

        // 체력이 0 이하 && 아직 죽지 않았다면 사망 처리 실행
        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    // 사망 처리
    public virtual void Die()
    {
        // onDeath 이벤트 등록된 메서드가 있다면 실행
        if (onDeath != null)
        {
            onDeath();
        }

        // 사망상태로 변경
        dead = true;
    }
}
