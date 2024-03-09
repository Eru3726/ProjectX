using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElisAvatar : MonoBehaviour, IDamageable
{
    public int Health { get; }

    [HideInInspector]
    public int num;

    [HideInInspector]
    public bool mainFlg = false;

    [HideInInspector]
    public ElisManager elisManager;

    [HideInInspector]
    public ElisData elisData;

    [HideInInspector]
    public GameObject bullet;

    [HideInInspector]
    public Transform playerTr;

    [SerializeField, Header("場所")]
    private Vector2[] position = new Vector2[4];

    private bool shotFlg = false;

    private readonly LineShot lineShot = new LineShot();

    private Rigidbody2D rb;

    private Vector2 targetPos;

    private bool damageableFlg = false;

    [SerializeField]
    private bool test = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;

        shotFlg = false;
        damageableFlg = false;

        targetPos = position[num];
    }

    void Update()
    {
        if (test) TakeDamage(1);
        //本体が攻撃を受けたら死亡
        if (elisManager.mianAvatarDeadFlg) Destroy(this.gameObject);

        //移動
        if (Vector2.Distance(transform.position, targetPos) > 0.1f)
        {
            // 現在位置から目標位置までの方向を取得
            Vector2 direction = ((Vector3)targetPos - transform.position).normalized;

            // 移動ベクトルを計算
            Vector2 moveVector = elisData.A_MoveSpeed * Time.deltaTime * direction;

            // Rigidbody2D に速度を適用
            rb.velocity = moveVector;
        }
        else
        {
            rb.velocity = Vector2.zero;
            damageableFlg = true;
            if (!elisManager.avatarStartFlg) return;
            else if (!shotFlg)
            {
                shotFlg = true;
                StartCoroutine(Shot());
            }
        }
    }

    /// <summary>
    /// 魔法弾
    /// </summary>
    /// <returns></returns>
    private IEnumerator Shot()
    {
        for (int i = 0; i < elisData.A_ShotNum; i++)
        {
            lineShot.Shot(Instantiate(bullet, transform.position, Quaternion.identity), transform, playerTr, elisData);

            yield return new WaitForSeconds(elisData.A_ShotTime);
        }

        yield return new WaitForSeconds(elisData.A_ShotTime * 2);

        shotFlg = false;
    }


    /// <summary>
    /// ダメージ処理
    /// </summary>
    /// <param name="value"></param>
    public void TakeDamage(int value)
    {
        if (!damageableFlg) return;

        // 攻撃を受けた場合の処理
        if (mainFlg) elisManager.MainAvatarDead(value, this.transform.position);
        Destroy(this.gameObject);
    }
}
