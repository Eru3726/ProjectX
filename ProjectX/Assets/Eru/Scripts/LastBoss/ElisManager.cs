using System;
using System.Collections;
using UnityEngine;

public enum Elis_MoveType
{
    Entry = 0,      //0.登場
    Standby,        //1.待機状態
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

    [SerializeField, Header("右側の定位置")]
    private Vector2 rightPos;

    [SerializeField, Header("中央の定位置")]
    private Vector2 centerPos;

    [SerializeField, Header("左側の定位置")]
    private Vector2 leftPos;

    [SerializeField, Header("分身")]
    private GameObject avatarObj;

    [SerializeField, Header("地面のレイヤー")]
    private LayerMask groundLeyer;

    [SerializeField, Header("レイの長さ")]
    private float rayLength = 0.6f;

    private Elis_MoveType moveType;

    private Rigidbody2D rb;

    private int hp, attackPow, defensePow;

    private float moveSpeed, waitTime;

    private bool halfHP = false;

    private readonly LineShot lineShot = new LineShot();

    private bool shotFlg = false;

    private Vector2 targetPos;

    private bool moveFlg = false;

    private float[] probabilities; // 各数字の確率

    private float timer;

    [HideInInspector]
    public bool avatarStartFlg = false;

    [HideInInspector]
    public bool mianAvatarDeadFlg = false;

    private bool floatingFlg = false;
    private bool floatedFlg = false;
    private bool felldownFlg = false;

    private Vector2 directionF;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        hp = elisData.HitPoint;
        attackPow = elisData.FallingAttackPower;
        defensePow = elisData.DefensePower;
        moveSpeed = elisData.MoveSpeed;
        waitTime = elisData.WaitTime;

        if (playerTr == null) playerTr = GameObject.Find("Player").transform;

        //moveType = Elis_MoveType.Entry;
        moveType = Elis_MoveType.Standby;
        rb.gravityScale = 0;
        halfHP = false;
        shotFlg = false;
        moveFlg = false;
        avatarStartFlg = false;
        floatingFlg = false;
        floatedFlg = false;
        felldownFlg = false;
    }

    private void Update()
    {
        MoveTypeChange();
        SeePlayer();
    }

    /// <summary>
    /// プレイヤーの方向を見る
    /// </summary>
    private void SeePlayer()
    {
        if (playerTr.transform.position.x <= transform.position.x) transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        else transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }

    private void MoveTypeChange()
    {
        switch (moveType)
        {
            case Elis_MoveType.Entry:
                Entry();
                break;

            case Elis_MoveType.Standby:
                Standby();
                break;

            case Elis_MoveType.Move:
                Move();
                break;

            case Elis_MoveType.Shot:
                if (shotFlg) break;
                shotFlg = true;
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

    /// <summary>
    /// 待機
    /// </summary>
    private void Standby()
    {
        if (timer >= waitTime)
        {
            //ランダムで次の行動を決める
            moveType = (Elis_MoveType)Enum.ToObject(typeof(Elis_MoveType), GenerateRandomAction());
            timer = 0;
        }
        else timer += Time.deltaTime;
    }

    /// <summary>
    /// 移動
    /// </summary>
    private void Move()
    {
        if (!moveFlg)
        {
            moveFlg = true;

            //画面の左側にいるとき
            if (this.transform.position.x < 0) targetPos = rightPos;
            else targetPos = leftPos;
        }

        // 現在位置から目標位置までの方向を取得
        Vector2 direction = ((Vector3)targetPos - transform.position).normalized;

        // 移動ベクトルを計算
        Vector2 moveVector = moveSpeed * Time.deltaTime * direction;

        // Rigidbody2D に速度を適用
        rb.velocity = moveVector;

        // 目標位置に近づいたかどうかを判定
        if (Vector2.Distance(transform.position, targetPos) < 0.1f)
        {
            // 目標位置に到達したら移動を停止
            rb.velocity = Vector2.zero;

            moveType = Elis_MoveType.Standby;
            moveFlg = false;
        }
    }

    /// <summary>
    /// 魔法弾
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shot()
    {
        for (int i = 0; i < elisData.ShotNum; i++)
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
        moveType = Elis_MoveType.Move;
        shotFlg = false;
    }

    /// <summary>
    /// 分身
    /// </summary>
    private void Avatar()
    {
        if (avatarStartFlg) return;

        // 中央に向かう
        if (Vector2.Distance(transform.position, centerPos) > 0.1f)
        {
            // 現在位置から目標位置までの方向を取得
            Vector2 direction = ((Vector3)centerPos - transform.position).normalized;

            // 移動ベクトルを計算
            Vector2 moveVector = moveSpeed * Time.deltaTime * direction;

            // Rigidbody2D に速度を適用
            rb.velocity = moveVector;
        }
        //分身生成
        else
        {
            //本体の番号選出
            int rand = UnityEngine.Random.Range(0, 4);
            mianAvatarDeadFlg = false;

            for (int i = 0; i < 4; i++)
            {
                //分身生成
                GameObject obj = Instantiate(avatarObj, transform.position, Quaternion.identity);
                ElisAvatar ea = obj.GetComponent<ElisAvatar>();
                if (i == rand) ea.mainFlg = true;
                else ea.mainFlg = false;
                ea.num = i;
                ea.elisManager = GetComponent<ElisManager>();
                ea.elisData = elisData;
                ea.bullet = bullet;
                ea.playerTr = playerTr;
            }

            //画面外へ移動
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 10f, this.transform.position.z);
            rb.velocity = Vector2.zero;
            avatarStartFlg = true;
        }
    }

    /// <summary>
    /// 落下攻撃
    /// </summary>
    private void FallingAttack()
    {
        //画面外まで飛び上がる
        if (!floatedFlg)
        {
            if (!floatingFlg)
            {
                floatingFlg = true;

                //画面の左側にいるとき
                if (playerTr.transform.position.x <= transform.position.x) targetPos = new Vector2(transform.position.x - 1, transform.position.y + 5);
                else targetPos = new Vector2(transform.position.x + 1, transform.position.y + 5);
            }

            // 現在位置から目標位置までの方向を取得
            Vector2 direction = ((Vector3)targetPos - transform.position).normalized;

            // 移動ベクトルを計算
            Vector2 moveVector = elisData.F_MoveUpSpeed * Time.deltaTime * direction;

            // Rigidbody2D に速度を適用
            rb.velocity = moveVector;

            // 目標位置に近づいたかどうかを判定
            if (Vector2.Distance(transform.position, targetPos) < 0.1f)
            {
                // 目標位置に到達したら移動を停止
                rb.velocity = Vector2.zero;
                floatedFlg = true;
                floatingFlg = false;
                targetPos = playerTr.transform.position;

                //プレイヤーの真上付近まで移動
                if (targetPos.x < transform.position.x) transform.position = new Vector2(targetPos.x + 1f, transform.position.y);
                else transform.position = new Vector2(targetPos.x - 1f, transform.position.y);

                // 現在位置から目標位置までの方向を取得
                directionF = ((Vector3)targetPos - transform.position).normalized;
            }
        }
        //落下
        else
        {
            if (!felldownFlg)
            {
                // 移動ベクトルを計算
                Vector2 moveVector = elisData.F_MoveDownSpeed * Time.deltaTime * directionF;

                // Rigidbody2D に速度を適用
                rb.velocity = moveVector;

                //着地判定
                if (GroundCheck())
                {
                    felldownFlg = true;
                    rb.velocity = Vector2.zero;

                    //衝撃波
                }
            }
            else
            {
                //着地後の硬直
                if (timer >= elisData.F_WaitTime)
                {
                    moveType = Elis_MoveType.Move;
                    timer = 0;
                    floatedFlg = false;
                    felldownFlg = false;
                }
                else timer += Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// 形態変化
    /// </summary>
    private void FormChange()
    {

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
        else if (hp <= elisData.HitPoint / 2 && !halfHP) halfHP = true;
    }

    /// <summary>
    /// 行動パターン選択
    /// </summary>
    /// <returns></returns>
    private int GenerateRandomAction()
    {
        if (halfHP) probabilities = new float[] { 0.60f, 0.25f, 0.15f };
        else probabilities = new float[] { 0.70f, 0.30f, 0.00f };

        float rand = UnityEngine.Random.value; // 0.0から1.0までのランダムな値を生成

        // 累積確率を計算
        float cumulativeProbability = 0f;
        for (int i = 0; i < probabilities.Length; i++)
        {
            cumulativeProbability += probabilities[i];
            if (rand < cumulativeProbability)
            {
                return i + 3; // 数字のオフセットを考慮して返す
            }
        }

        return probabilities.Length;
    }

    /// <summary>
    /// 分身の本体が死んだとき
    /// </summary>
    /// <param name="value"></param>
    /// <param name="pos"></param>
    public void MainAvatarDead(int value, Vector3 pos)
    {
        this.transform.position = pos;
        TakeDamage(value);
        mianAvatarDeadFlg = true;
        moveType = Elis_MoveType.Move;
        avatarStartFlg = false;
    }


    private bool GroundCheck()
    {
        // レイの始点をこのオブジェクトの位置に設定
        Vector2 origin = transform.position;

        // レイの方向を真下に設定
        Vector2 direction = Vector2.down;

        // レイキャストを実行し、当たったオブジェクトの情報を取得
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, rayLength, groundLeyer);
        Debug.DrawRay(origin, direction * rayLength, Color.red);

        return hit.collider != null;
    }
}
