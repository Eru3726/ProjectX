using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlHitCol : MonoBehaviour, IDamageable, IShockable, IInvincible
{
    [SerializeField] int maxHp = 20;
    [SerializeField] int nowHp;
    [SerializeField] float resist = 1;
    [SerializeField] float inv = 0;

    [SerializeField]
    public int barrier = 0;

    [SerializeField, Header("mcアタッチ")]
    public MoveController mc;

    [SerializeField, Header("本体アタッチ")]
    public GameObject body;

    public int Health => nowHp;

    public float shockResist => resist;

    public float invTime => inv;

    public void TakeDamage(int value)
    {
        if (inv <= 0)
        {
            nowHp -= value;

            if (nowHp <= 0)
            {
                Die();
            }
        }
    }

    public void TakeShock(float value, Vector3 pos)
    {
        if (inv <= 0)
        {
            Vector3 shockDir = pos - this.transform.position;

            if (mc != null)
            {
                mc.InputFlick(this.transform.position - shockDir, value * resist, 0.5f, false);
            }
        }
    }

    public void TakeInv(float value)
    {
        if (inv <= 0)
        {
            inv = value;
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

    void FixedUpdate()
    {
        if (inv > 0)
        {
            inv -= Time.deltaTime;
        }
        else
        {
            inv = 0;
        }
    }
}
