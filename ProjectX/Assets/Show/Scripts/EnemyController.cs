using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
   
    MoveController mc;
   
    MoveController plmc;

    
    Rigidbody2D rb;

    [Header("Enemyの挙動")]
    public EnemyState currentState;
    [Header("Playerとの距離")]
    public int Atkdis = 4;
    [Header("Warpのbool型変数")]
    public bool movecheck = true;
    [Header("Moveのbool型変数")]
    public bool warpcheck = true;

    public float minX = 3f;  // 移動可能なX座標の最小値
    public float maxX = 6f;   // 移動可能なX座標の最大値
    public float minY = 4f;   // 移動可能なY座標の最小値
    public float maxY = 6f;    // 移動可能なY座標の最大値

    [Header("Enemyの弾Prefab")]
    public GameObject ShellPre;

   
    float warpDelay = 1f; //ワープするまでの時間
    float idolDelay = 1.5f; //待機時間


    public enum EnemyState
    {
        Idol,
        Idol2,
        Warp,
        Warp2,
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
        plmc = player.gameObject.GetComponent<MoveController>();
    }

    void FixedUpdate()
    {
        switch (currentState)
        {

            case EnemyState.Idol: //次の行動に移るための待機

                movecheck = true;
                warpcheck = true;
                currentState = EnemyState.Idol2;
                StartCoroutine(IdolDelay());
                break;

            case EnemyState.Idol2: //保護
                break;

            case EnemyState.Move: //ワープの処理
                Debug.Log("Move");

                EnemyMove(); //Enemyの通常時の動き

                break;

            case EnemyState.Homing:　//ホーミング攻撃の処理

                Debug.Log("Homing");
                EnemyHoming();
                break;

            case EnemyState.Dash:　//突進の処理

                Debug.Log("Dash");

                EnemyDash();
                break;

            case EnemyState.Warp:  //移動
                Debug.Log("Warp");
                currentState = EnemyState.Warp2;

                StartCoroutine(WarpDelay());
                break;

            case EnemyState.Warp2:
                break;

            case EnemyState.Down:　//ダウン


                break;
            case EnemyState.Die:　//消滅


                break;
        }
    }

    IEnumerator IdolDelay()
    {
        Debug.Log("待機中");
        yield return new WaitForSeconds(idolDelay);
        currentState = EnemyState.Warp;
    }

    IEnumerator WarpDelay()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(warpDelay);
        EnemyWarp();
    }

    public void EnemyMove()
    {
        if (movecheck && plmc.IsGround())
        {
            Debug.Log("a");
            Vector2 playerPos = player.transform.position;
            Vector2 enemyPos = transform.position;
            //Vector2 pos = new Vector2(playerPos.x, 0);
            float directionX = playerPos.x - transform.position.x;

            // rb.velocity = new Vector2(directionX, rb.velocity.y).normalized * 5.0f;

            mc.InputLR((int)Mathf.Sign(directionX));

            float dis = Vector2.Distance(playerPos, enemyPos);

            if (dis <= Atkdis)
            {
                mc.InputLR(0);

                Debug.Log("追従");
                currentState = EnemyState.Dash;

            }
        }
        
    }

    public void EnemyDash()
    {
        Debug.Log("突進");
        mc.InputFlick(player.transform.position, 30, 0.3f, true);
        currentState = EnemyState.Idol;
    }

    public void EnemyWarp()
    {
        if (warpcheck)
        {
            this.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);
            Debug.Log(this.GetComponent<SpriteRenderer>().color);

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
            transform.position = new Vector3(randomX, randomY, 0);

            warpcheck = false;
        }
        currentState = EnemyState.Homing;
    }
    public void EnemyHoming()
    {
        Vector2 enemyPos = transform.position;

        Instantiate(ShellPre,enemyPos, Quaternion.identity);
        Instantiate(ShellPre,enemyPos, Quaternion.identity);
        Instantiate(ShellPre,enemyPos, Quaternion.identity);
        Instantiate(ShellPre,enemyPos, Quaternion.identity);
        Instantiate(ShellPre,enemyPos, Quaternion.identity);
        Instantiate(ShellPre,enemyPos, Quaternion.identity);
        Instantiate(ShellPre,enemyPos, Quaternion.identity);
        Instantiate(ShellPre,enemyPos, Quaternion.identity);

        currentState = EnemyState.Move;
    }
    public void EnemyDown()
    {

    }
    public void EnemyDie()
    {

    }
}
