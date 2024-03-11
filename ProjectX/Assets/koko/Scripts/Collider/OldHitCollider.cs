using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OldHitCollider : MonoBehaviour, IDamageable
{
    public int Health { get; }
    public void TakeDamage(int value) { }

    [SerializeField, Header("MoveControllerをアタッチ（ノックバックしないなら不要）")]
    MoveController mc;

    [SerializeField, Header("親オブジェクトをアタッチ（死亡時にDestroy）")]
    GameObject parent;

    [SerializeField, Header("最大HP")]
    protected int maxHp = 20;

    [SerializeField, Header("現在HP（設定不要）")]
    protected int nowHp;

    [SerializeField, Header("バリア")]
    protected int barrier = 0;

    [SerializeField, Header("防御力（引き算）")]
    protected int defence = 0;

    [SerializeField, Header("衝撃耐性（倍率）")]
    protected float resist = 1;

    [SerializeField, Header("被撃レイヤー")]
    protected SkillData.LAYER_DATA hitLayer;

    SpriteRenderer sr;

    // attackタイプ別無敵時間
    public List<float> invTime = new List<float>();

    private void Start()
    {
        nowHp = maxHp;

        for (int i = 0; i < (int)SkillData.ATK_DATA.Num; i++)
        {
            invTime.Add(0);
        }

        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        UpdateInv();

        if(barrier <= 0)
        {
            sr.color = new Color32(0, 255, 0, 64);
        }
        else
        {
            sr.color = new Color32(0, 255, 255, 64);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckHitLayer(collision);
    }

    protected void Damage(int dmg)
    {
        // 与ダメ計算
        int trueDmg;
        trueDmg = dmg - defence;
        if(trueDmg <= 0) { trueDmg = 1; }

        // バリア計算
        if (barrier > 0)
        {
            barrier -= trueDmg;
        }
        else
        {
            nowHp -= trueDmg;
        }

        if (barrier <= 0) { barrier = 0; }

        // 死亡判定
        if (nowHp <= 0)
        {
            Die();
        }
    }

    protected void Shock(float shock, Vector3 pos)
    {
        // 衝撃付与
        Vector3 shockDir = pos - this.transform.position;

        if (mc != null)
        {
            mc.InputFlick(this.transform.position - shockDir, shock * resist, 0.5f, false);
        }
    }

    protected void Die()
    {
        Destroy(parent.gameObject);
    }

    protected void UpdateInv()
    {
        for (int i = 0; i < (int)SkillData.ATK_DATA.Num; i++)
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
        // 当たったコライダーがattackコライダーを持ってるなら取得
        if (collision.TryGetComponent<OldAttackCollider>(out OldAttackCollider atk))
        {
            // 攻撃レイヤーと被撃レイヤーが違うかつ、その攻撃に対応する無敵時間が0の場合のみダメージ処理へ
            if (atk.atkLayer != hitLayer && invTime[(int)atk.atkType] <= 0)
            {
                // 無敵付与
                invTime[(int)atk.atkType] = atk.inv;

                // ダメージ
                Damage(atk.dmg);
                Shock(atk.shock, atk.apPos);
            }
        }
    }

    public void SetInvTime(float time)
    {
        for (int i = 0; i < (int)SkillData.ATK_DATA.Num; i++)
        {
            invTime[i] = time;
        }
    }

    public void SetBarrier(int value)
    {
        barrier = value;
    }

    public SkillData.LAYER_DATA GetHitLayer()
    {
        return hitLayer;
    }
}
