using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GurenHitCol : MonoBehaviour, IDamageable
{
    [SerializeField] private int maxHp = 10;
    [SerializeField] private int nowHp;
    [SerializeField] private bool halfHp = false;
    [SerializeField] private float resist = 1;

    [SerializeField] private Behaviour _target;
    // 点滅周期[s]
    [SerializeField] private float _cycle = 1;

    private double _time;

    SpriteRenderer sp;


    [SerializeField, Header("mcアタッチ")]
    public MoveController mc;

    public EnemyController ec;

    public HPBackBar hpbar;

    [SerializeField, Header("本体アタッチ")]
    public GameObject body;

    public int Health => nowHp;

    public float shockResist => resist;

    public void TakeDamage(int value)
    {
        nowHp -= value;
        hpbar.UpdateHP(10);

        if (nowHp <= 0)
        {
            ec.EnemyDown();
        }
        else if(nowHp <= 5)
        {
            halfHp = true;
        }
    }
    void Start()
    {
        nowHp = maxHp;
    }

    void FixedUpdate()
    {
        
    }
}
