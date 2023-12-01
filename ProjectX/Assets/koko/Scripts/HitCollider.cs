using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    [SerializeField, Header("MoveControllerをアタッチ（ノックバックしないなら不要）")]
    MoveController mc;

    [SerializeField, Header("親オブジェクトをアタッチ（死亡時にDestroy）")]
    GameObject parent;

    [SerializeField, Header("最大HP")]
    protected float maxHp = 20;

    [SerializeField, Header("現在HP（設定不要）")]
    protected float nowHp;

    [SerializeField, Header("防御力（引き算）")]
    protected float defence = 0;

    [SerializeField, Header("衝撃耐性（倍率）")]
    protected float resist = 1;

    [SerializeField, Header("被撃レイヤー")]
    protected StageData.LAYER_DATA hitLayer;

    // attackタイプ別無敵時間
    public List<float> invTime = new List<float>();

    private void Start()
    {
        nowHp = maxHp;

        for (int i = 0; i < (int)StageData.ATK_DATA.Num; i++)
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
        float trueDmg;
        trueDmg = dmg - defence;
        if(trueDmg <= 0) { trueDmg = 1; }

        nowHp -= trueDmg;

        Vector3 shockDir = pos - this.transform.position;
        if (mc != null)
        {
            mc.InputFlick(this.transform.position - shockDir, shock * resist, 0.5f, false);
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
            if (atk.atkLayer != hitLayer && invTime[(int)atk.atkType] <= 0)
            {
                Damage(atk.dmg, atk.shock, atk.transform.position);
                invTime[(int)atk.atkType] = 1;
            }
        }
    }

    public void SetInvTime(float time)
    {
        for (int i = 0; i < (int)StageData.ATK_DATA.Num; i++)
        {
            invTime[i] = time;
        }
    }
}
