using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GurenHitCol : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 10;
    [SerializeField] private int nowHp;
    [SerializeField] private bool halfHp = false;
    [SerializeField] private float resist = 1;

    [SerializeField, Header("mcアタッチ")]
    public MoveController mc;

    public EnemyController ec;

    [SerializeField, Header("本体アタッチ")]
    public GameObject body;

    public int Health => nowHp;

    public float shockResist => resist;

    public void TakeDamage(int value)
    {
        nowHp -= value;

        if (nowHp <= 0)
        {
            ec.EnemyDie();
        }
        else if(nowHp <= 5)
        {
            halfHp = true;
        }
    }

    //void Die()
    //{
    //    if (body != null)
    //    {
            
    //        Destroy(body.gameObject);
    //    }
    //}

    void Start()
    {
        nowHp = maxHp;
    }

    void FixedUpdate()
    {
        
    }
}
