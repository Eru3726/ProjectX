using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EmHitCol : MonoBehaviour, IDamageable, IShockable
{
    [SerializeField] int maxHp = 20;
    [SerializeField] int nowHp;
    [SerializeField] float resist = 1;

    [SerializeField, Header("mcアタッチ")]
    public MoveController mc;

    [SerializeField, Header("本体アタッチ")]
    public GameObject body;

    [SerializeField, Header("dieEffectアタッチ")]
    GameObject edec;

    [SerializeField, Header("DamageCounterアタッチ")]
    GameObject damageCounter;

    public int Health => nowHp;

    public float shockResist => resist;

    public void TakeDamage(int value)
    {
        nowHp -= value;

        // ダメージカウンター生成
        if (damageCounter != null)
        {
            float randX = Random.Range(-0.5f, 0.5f);
            float randY = Random.Range(-0.5f, 0.5f);
            Vector3 pos = new Vector3(transform.position.x + randX, transform.position.y + randY, transform.position.z);
            GameObject obj = Instantiate(damageCounter, pos, Quaternion.identity);
            obj.transform.GetChild(0).gameObject.GetComponent<Text>().text = value.ToString();
        }

        // ヒットストップ
        GameObject tm = GameObject.Find("TimeManager");
        if (tm.TryGetComponent<TimeManager>(out TimeManager manager))
        {
            manager.SetSlow(0.1f, 0.01f);
        }

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
        if (mc != null)
        {
            mc.InputFlickStop();
        }
    }

    void Die()
    {
        if (edec != null)
        {
            Instantiate(edec, transform.position, Quaternion.identity);
        }

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
