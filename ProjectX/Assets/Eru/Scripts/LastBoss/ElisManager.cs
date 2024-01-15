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

    [SerializeField]
    private ElisData elisData;

    private Elis_MoveType moveType;

    private Rigidbody2D rb;

    private int hp, attackPow, defensePow;

    private float moveSpeed;

    private bool halfHP = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        hp = elisData.ElisData_HitPoint;
        attackPow = elisData.ElisData_FallingAttackPower;
        defensePow = elisData.ElisData_DefensePower;
        moveSpeed = elisData.ElisData_MoveSpeed;

        Debug.Log(hp);
        Debug.Log(attackPow);
        Debug.Log(defensePow);
        Debug.Log(moveSpeed);

        moveType = Elis_MoveType.Entry;
        rb.gravityScale = 0;
        halfHP = false;
    }

    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="value"></param>
    public void TakeDamage(int value)
    {
        hp -= value;
        if (hp <= 0)
        {
            // Healthが0になった場合の処理
            moveType = Elis_MoveType.FormChange;
        }
        else if (hp <= elisData.ElisData_HitPoint / 2 && !halfHP) halfHP = true;
    }
}
