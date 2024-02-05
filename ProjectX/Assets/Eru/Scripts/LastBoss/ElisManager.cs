using System.Collections;
using UnityEngine;

public enum Elis_MoveType
{
    Entry = 0,      //0.登場
    Neutral,        //1.待機状態
    Move,           //2.移動
    Shot,           //3.魔法弾攻撃
    Avatar,         //4.分裂
    FallingAttack,  //5.落下攻撃
    FormChange,     //6.形態変化
}

public class ElisManager : MonoBehaviour, IDamageable
{
    public int Health => hp;

    [SerializeField, Header("マスターデータ")]
    private ElisData elisData;

    [SerializeField, Header("魔法弾")]
    private GameObject bullet;

    [SerializeField, Header("プレイヤーのTR")]
    private Transform playerTr;

    private Elis_MoveType moveType;

    private Rigidbody2D rb;

    private int hp, attackPow, defensePow;

    private float moveSpeed;

    private bool halfHP = false;

    private readonly LineShot lineShot = new LineShot();

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        hp = elisData.HitPoint;
        attackPow = elisData.FallingAttackPower;
        defensePow = elisData.DefensePower;
        moveSpeed = elisData.MoveSpeed;

        if (playerTr == null) playerTr = GameObject.Find("Player").transform;

        //moveType = Elis_MoveType.Entry;
        moveType = Elis_MoveType.Shot;
        rb.gravityScale = 0;
        halfHP = false;

        MoveTypeChange();
    }

    private void MoveTypeChange()
    {
        switch (moveType)
        {
            case Elis_MoveType.Entry:
                Entry();
                break;

            case Elis_MoveType.Neutral:
                Neutral();
                break;

            case Elis_MoveType.Move:
                Move();
                break;

            case Elis_MoveType.Shot:
                StartCoroutine(Shot());
                break;

            case Elis_MoveType.Avatar:
                Avatar();
                break;

            case Elis_MoveType.FallingAttack:
                FallingAttack();
                break;

            case Elis_MoveType.FormChange:
                FormChange();
                break;
        }
    }

    private void Entry()
    {

    }

    private void Neutral()
    {

    }

    private void Move()
    {

    }

    private IEnumerator Shot()
    {
        for (int i = 0;i< elisData.ShotNum; i++)
        {
            lineShot.Shot(Instantiate(bullet, transform.position, Quaternion.identity), transform, playerTr, elisData);

            yield return new WaitForSeconds(elisData.ShotTime);
        }

        //体力が半分の時は追撃
        if (halfHP)
        {
            for (int i = 0; i < elisData.ShotNum; i++)
            {
                lineShot.Shot(Instantiate(bullet, transform.position, Quaternion.identity), transform, playerTr, elisData);

                yield return new WaitForSeconds(elisData.ShotTime / 2f);
            }
        }

        yield return new WaitForSeconds(2f);
        MoveTypeChange();
    }

    private void Avatar()
    {

    }

    private void FallingAttack()
    {

    }

    private void FormChange()
    {

    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="value"></param>
    public void TakeDamage(int damage, float shock)
    {
        hp -= damage;
        if (hp <= 0)
        {
            // Healthが0になった場合の処理
            moveType = Elis_MoveType.FormChange;
            MoveTypeChange();
        }
        else if (hp <= elisData.HitPoint / 2 && !halfHP) halfHP = true;
    }
}
