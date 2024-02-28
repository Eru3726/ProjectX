using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlHitCol : MonoBehaviour, IDamageable, IShockable, IInvincible
{
    [SerializeField] int maxHp = 20;
    [SerializeField] int nowHp;
    [SerializeField] float resist = 1;
    [SerializeField] public float time = 0;

    [SerializeField]
    public int barrier = 0;

    [SerializeField, Header("mcアタッチ")]
    public MoveController mc;

    [SerializeField, Header("本体アタッチ")]
    public GameObject body;

    public int Health => nowHp;

    public float shockResist => resist;

    public float invTime => time;

    public void TakeDamage(int value)
    {
        if (time <= 0)
        {
            if (barrier > 0)
            {
                barrier -= value;
                if (barrier <= 0)
                {
                    barrier = 0;
                }
            }
            else
            {
                nowHp -= value;
            }

            if (nowHp <= 0)
            {
                Die();
            }
        }
    }

    public void TakeShock(float value, Vector3 pos)
    {
        if (time <= 0)
        {
            Vector3 shockDir = pos - this.transform.position;

            if (mc != null)
            {
                mc.InputFlick(this.transform.position - shockDir, value * resist, 0.5f, false);
            }
        }
    }

    public void TakeStop() { }

    public void TakeInv(float value)
    {
        if (time <= 0)
        {
            time = value;
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
        if (time > 0)
        {
            time -= Time.deltaTime;
        }
        else
        {
            time = 0;
        }
    }
}
