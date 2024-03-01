using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    [SerializeField] float ShellSpd = 7f;
    [SerializeField] float rotSpd = 1f;

    GameObject player;
    Vector3 playerPos;
    Rigidbody2D rb;

    public EnemyController ec;

    public float ShellupSpd = 5;

    bool ShellFlg = false;

    float radian;
    int count;
    Vector3 shellVec;

    float time = 1.0f;
    float timer = 1.0f;
    float distance = 0.1f;

    void Start()
    {
        player = GameObject.Find("Player");
        playerPos = player.transform.position;
        rb = GetComponent<Rigidbody2D>();

        radian = Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x);

        shellVec.x = ShellSpd * Time.deltaTime * Mathf.Cos(radian);
        shellVec.y = ShellSpd * Time.deltaTime * Mathf.Sin(radian);
        Vector3 dis = player.transform.position - transform.position;

        if (dis.x >= -distance)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 1);
        }
        if (dis.x <= distance)
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
        }
    }


    void FixedUpdate()
    {
      
        time -= Time.deltaTime;
        if (time > 0)
        {
            transform.Translate(Vector3.up * ShellupSpd * Time.deltaTime);
        }
        else
        {
            shellVec.x = ShellSpd * Time.deltaTime * Mathf.Cos(radian);
            shellVec.y = ShellSpd * Time.deltaTime * Mathf.Sin(radian);

            this.transform.Translate(shellVec);

            // 外積を求めるために、ベクトルを作成する
            // 弾の位置と、PLの位置のベクトル
            Vector3 plVec = playerPos - transform.position;
            // 弾の速度ベクトル
            Vector3 spdVec = shellVec;
            // 外積を求める
            float cross = plVec.x * spdVec.y - spdVec.x * plVec.y;

            if (cross > 0)
            {
                radian -= rotSpd * Time.deltaTime;  // 反時計回りさせる
            }
            else
            {
                radian += rotSpd * Time.deltaTime;  // 時計回りさせる

            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
           ec.ishoming = true;
           Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("消えた");
        ec.ishoming = true;
        Destroy(this.gameObject);
    }
}
