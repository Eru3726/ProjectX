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

    // 無敵時間
    protected float invTime;

    private void Start()
    {
        nowHp = maxHp;
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
        if (invTime <= 0)
        {
            nowHp -= dmg;

            Vector3 shockDir = pos - this.transform.position;

            mc.InputFlick(this.transform.position - shockDir, shock * 2, 1, true);

            invTime = 1;
        }
        else
        {
            // Debug.Log("inv:" + this.gameObject);
        }

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
        if (invTime > 0)
        {
            invTime -= Time.deltaTime;
        }
        else
        {
        }
    }

    protected void CheckHitLayer(Collider2D collision)
    {
        if (collision.TryGetComponent<AttackCollider>(out AttackCollider atk))
        {
            if (atk.atkLayer != hitLayer)
            {
                Damage(atk.dmg, atk.shock, atk.transform.position);
            }
        }
    }



    public void SetInvTime(float time)
    {
        invTime = time;
    }
}
