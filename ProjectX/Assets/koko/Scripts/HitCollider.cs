using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField, Header("MoveControllerをアタッチ")]
    MoveController mc;

    [SerializeField, Header("親オブジェクトをアタッチ")]
    GameObject parent;

    [SerializeField, Header("最大HP")]
    protected float maxHp = 20;

    [SerializeField, Header("現在HP（設定不要）")]
    protected float nowHp;

    [SerializeField, Header("被撃レイヤー")]
    protected int hitLayer = 0;

    // attackタイプ別無敵時間
    public List<float> invTime = new List<float>();

    private void Start()
    {
        nowHp = maxHp;

        for (int i = 0; i < 10; i++)
        {
            invTime.Add(0);
        }
    }

    private void FixedUpdate()
    {
        UpdateInv();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckHitLayer(collision);
    }

    protected void Damage( float dmg, float shock, Vector3 pos)
    {

        nowHp -= dmg;

        Vector3 shockDir = pos - this.transform.position;

        mc.InputFlick(this.transform.position - shockDir, shock * 2, 0.5f, true);

        if (nowHp <= 0)
        {
            Die();
        }
    }

    protected void Die()
    {
        Destroy(parent.gameObject);
    }

    protected void UpdateInv()
    {
        for (int i = 0; i <10; i++)
        {
            if (invTime[i] > 0)
            {
                invTime[i] -= Time.deltaTime;
            }
            else
            {
                invTime[i] = 0;
            }
        }
    }

    protected void CheckHitLayer(Collider2D collision)
    {
        if (collision.TryGetComponent<AttackCollider>(out AttackCollider atk))
        {
            if (atk.atkLayer != hitLayer && invTime[atk.atkType] <= 0)
            {
                Damage(atk.dmg, atk.shock, atk.transform.position);
                invTime[atk.atkType] = 1;
            }
        }
    }

    public void SetInvTime(float time)
    {
        for (int i = 0; i < 10; i++)
        {
            invTime[i] = time;
        }
    }
}
