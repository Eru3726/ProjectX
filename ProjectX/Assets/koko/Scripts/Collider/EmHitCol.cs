using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmHitCol : MonoBehaviour, IDamageable
{
    [SerializeField] int maxHp = 10;
    [SerializeField] int nowHp;
    [SerializeField] float resist = 1;

    [SerializeField, Header("mcアタッチ")]
    public MoveController mc;

    [SerializeField, Header("本体アタッチ")]
    public GameObject body;

    public int Health => nowHp;

    public float shockResist => resist;

    public void TakeDamage(int value)
    {
        nowHp -= value;

        if (nowHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (body != null)
        {
            Destroy(body.gameObject);
        }
    }

    void Start()
    {
        nowHp = maxHp;
    }
}
