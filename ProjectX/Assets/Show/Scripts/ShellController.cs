using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TYPE
{
    Up,
    Rotate,
    Fly
}

public class ShellController : MonoBehaviour
{
    [HideInInspector]
    public EnemyController ec;

    [HideInInspector]
    public MoveController mc;

    [HideInInspector]
    public int num;

    [SerializeField]
    private float moveSpeed = 4f;

    [SerializeField]
    private float upSpeed = 2f;

    [SerializeField]
    private float rotateSpeed = 3f;

    [SerializeField]
    private int attackPow = 1;

    [SerializeField]
    private GameObject afterImage;

    [SerializeField]
    private float maxX = 0.5f, minX = -0.5f;

    private float offsetY = 0.8f;

    private Vector2 playerPos;
    private Rigidbody2D rb;
    private TYPE type;

    private bool moveFlg = false;
    private Vector2 targetPos;

    private void Start()
    {
        playerPos = GameObject.Find("Player").transform.position;
        rb = GetComponent<Rigidbody2D>();
        moveFlg = false;
        mc = GetComponent<MoveController>();
        type = TYPE.Up;
    }

    private void Update()
    {
        switch (type)
        {
            case TYPE.Up:
                Up();
                break;
            case TYPE.Rotate:
                Rotate();
                break;
            case TYPE.Fly:
                Fly();
                break;
        }
    }

    private void Up()
    {
        if (!moveFlg)
        {
            moveFlg = true;

            targetPos = new Vector2(Random.Range(maxX, minX), this.transform.position.y + offsetY);
        }

        // 現在位置から目標位置までの方向を取得
        Vector2 direction = ((Vector3)targetPos - transform.position).normalized;

        // 移動ベクトルを計算
        Vector2 moveVector = upSpeed * direction;

        // Rigidbody2D に速度を適用
        rb.velocity = moveVector;

        // 目標位置に近づいたかどうかを判定
        if (Vector2.Distance(transform.position, targetPos) < 0.2f)
        {
            // 目標位置に到達したら移動を停止
            rb.velocity = Vector2.zero;
            targetPos = playerPos;
            moveFlg = false;

            type = TYPE.Rotate;
        }
    }

    private void Rotate()
    {
        // プレイヤーの位置を向く方向を計算
        Vector3 direction = ((Vector3)targetPos - transform.position).normalized;

        // 向く方向の角度を計算
        float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // 敵のZ軸の回転角度を設定
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, targetAngle - 90f);

        // 敵を向く方向に少しずつ回転させる
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed);

        // 敵がプレイヤーの方向を向いているかどうかを判定
        if (Quaternion.Angle(transform.rotation, targetRotation) < 1f)
        {
            targetPos = ((Vector3)targetPos - transform.position).normalized;

            type = TYPE.Fly;
        }
    }

    private void Fly()
    {
        // 移動ベクトルを計算
        Vector2 moveVector = moveSpeed  * targetPos;
        Instantiate(afterImage, transform.position, transform.rotation);
        // Rigidbody2D に速度を適用
        rb.velocity = moveVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("消えた");
            ec.ishoming[num] = true;
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("消えた");
            ec.ishoming[num] = true;
            Destroy(this.gameObject);
        }
    }
}
