using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmHitCol : MonoBehaviour, IDamageable, IShockable
{
    [SerializeField] int maxHp = 20;
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

    public void TakeShock(float value, Vector3 pos)
    {
        Vector3 shockDir = pos - this.transform.position;

        if (mc != null)
        {
            mc.InputFlick(this.transform.position - shockDir, value * resist, 0.5f, false);
        }
    }

    public void TakeStop() 
    {
        mc.InputFlickStop();
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
