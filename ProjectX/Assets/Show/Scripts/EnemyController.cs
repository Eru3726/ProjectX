using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    MoveController mc;

    Rigidbody2D rb;

    [SerializeField] EnemyAtk enemyAtk;

    public EnemyState currentState;
    public int Atkdis = 4;
    public bool tracking = true;
    public bool warpcheck = true;

    public float minX = 3f;  // 移動可能なX座標の最小値
    public float maxX = 6f;   // 移動可能なX座標の最大値
    public float minY = 4f;   // 移動可能なY座標の最小値
    public float maxY = 6f;    // 移動可能なY座標の最大値
    float warpdelay = 1f; //ワープするまでの時間

    public enum EnemyState
    {
        Idol,
        Warp,
        Homing,
        Dash,
        Move,
        Down,
        Die,
        Num
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        currentState = EnemyState.Move;
        mc = gameObject.GetComponent<MoveController>();
    }

    void FixedUpdate()
    {
        Debug.Log(mc.GetLR());

        switch (currentState)
        {

            case EnemyState.Idol:　//次の行動に移るための待機

                StartCoroutine(WaitForSomeTime(1f));

                break;

            case EnemyState.Move: //ワープの処理

                EnemyMove(); //Enemyの通常時の動き

                break;

            case EnemyState.Homing:　//ホーミング攻撃の処理

                Debug.Log("Homing");
                EnemyHoming();
                break;

            case EnemyState.Dash:　//突進の処理

                EnemyDash();
                break;

            case EnemyState.Warp:  //移動

                Debug.Log("Warp");
                StartCoroutine(WarpDelay());

                break;
            case EnemyState.Down:　//ダウン


                break;
            case EnemyState.Die:　//消滅


                break;
        }
    }

    IEnumerator WaitForSomeTime(float seconds)
    {
        Debug.Log("待機中");
        yield return new WaitForSeconds(seconds);
        mc.InputLR(0);
        currentState = EnemyState.Warp;
    }

    public void EnemyMove()
    {
        if (tracking)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 enemyPos = transform.position;
            //Vector2 pos = new Vector2(playerPos.x, 0);
            float directionX = playerPos.x - transform.position.x;

            // rb.velocity = new Vector2(directionX, rb.velocity.y).normalized * 5.0f;

            mc.InputLR((int)Mathf.Sign(directionX));

            float dis = Vector2.Distance(playerPos, enemyPos);

            if (dis <= Atkdis)
            {
                Debug.Log("追従");
                currentState = EnemyState.Dash;
            }
        }
    }

    public void EnemyDash()
    {
        Debug.Log("突進");
        enemyAtk.EnemyAttack();
        mc.InputFlick(player.transform.position, 20, 0.3f, true);

        currentState = EnemyState.Idol;
    }

    public void EnemyWarp()
    {
        if (warpcheck)
        {
            this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, 6239);
            Debug.Log(this.GetComponent<SpriteRenderer>().color);
            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            transform.position = new Vector3(randomX, randomY, 0);
            warpcheck = false;
        }
        currentState = EnemyState.Homing;
    }

    IEnumerator WarpDelay()
    {
        this.GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -120);
        yield return new WaitForSeconds(warpdelay);
        EnemyWarp();
    }
    public void EnemyHoming()
    {

    }
}
