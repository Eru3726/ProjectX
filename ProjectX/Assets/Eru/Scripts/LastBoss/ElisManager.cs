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

public class ElisManager : MonoBehaviour
{
    [SerializeField,Tooltip("ボスデータ")]
    private LastBossData lastBossData;

    private Elis_MoveType moveType;

    private Rigidbody2D rb;

    private int hp, attackPow, defensePow;

    private float moveSpeed;


    void Awake()
    {
        Debug.Log(lastBossData.ElisFastData[0].fallingAttackPower);
        Debug.Log(lastBossData.ElisFastData[0].b_MoveTime);

        rb = GetComponent<Rigidbody2D>();

        hp = lastBossData.ElisFastData[0].hitPoint;
        attackPow = lastBossData.ElisFastData[0].fallingAttackPower;
        defensePow = lastBossData.ElisFastData[0].defensePower;
        moveSpeed = lastBossData.ElisFastData[0].moveSpeed;

        moveType = Elis_MoveType.Entry;
        rb.gravityScale = 0;
    }
}
