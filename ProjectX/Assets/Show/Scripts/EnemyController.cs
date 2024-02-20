using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;

    Animator animator;
   
    MoveController mc;
   
    MoveController plmc;

    ShellController sc;

    public EnemyAnimController eac;

    Rigidbody2D rb;

    [Header("Enemyの挙動")]
    public EnemyState currentState;
    [Header("Playerとの距離")]
    public int Atkdis = 4;
    [Header("Warpのbool型変数")]
    public bool movecheck = true;
    [Header("Moveのbool型変数")]
    public bool warpcheck = true;

    public bool FingerFlg = false;
    public bool MoveFlg = false;
    public bool DashFlg = false;
    public bool WarpFlg = false;
    public bool HomingFlg = false;
    public bool DownFlg = false;

    public bool nowDashFlg = false;

    public float minX = 3f;  // 移動可能なX座標の最小値
    public float maxX = 6f;   // 移動可能なX座標の最大値
    public float minY = 4f;   // 移動可能なY座標の最小値
    public float maxY = 6f;    // 移動可能なY座標の最大値

    float timer = 0;

    [Header("Enemyの弾Prefab")]
    public GameObject ShellPre;

    int eight = 8;
    int FingerMovecount = 0;
    int FingerHomcount = 0;
 
    float warpDelay = 1f; //ワープするまでの時間
    float idolDelay = 1.5f; //待機時間

    private bool nowHomingFlg = false;
    public bool ishoming = false;


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
        sc = gameObject.GetComponent<ShellController>();
        eac = gameObject.GetComponent<EnemyAnimController>();
        animator = gameObject.GetComponent<Animator>();
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

    void EnemyMove()
    {
        if (movecheck && plmc.IsGround())
        {
            if(FingerMovecount <= 0)
            {
                FingerFlg = true;
                FingerMovecount++;
            }
            if(FingerFlg)
            {
                FingerMovecount = 0;
            }
            Vector2 playerPos = player.transform.position;
            Vector2 enemyPos = transform.position;
            float directionX = playerPos.x - transform.position.x;

            mc.InputLR((int)Mathf.Sign(directionX));

            float dis = Vector2.Distance(playerPos, enemyPos);

            if (dis <= Atkdis)
            {
                mc.InputLR(0);
                currentState = EnemyState.Dash;
            }

        }
        
    }

    void EnemyDash()
    {
        Debug.Log("突進");
        mc.InputFlick(player.transform.position, 20, 0.3f, true);
        FingerFlg = false;
        DashFlg = true;
        currentState = EnemyState.Idol;
    }

    void EnemyWarp()
    {
        Debug.Log("a");
        animator.Play("Guren_FSAnimation");

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
    void EnemyHoming()
    {
        FingerFlg = true;
        if (ishoming)
        {
            currentState = EnemyState.Move;
            nowHomingFlg = false;
            ishoming = false;
            return;
        }
        if (nowHomingFlg) return;

        nowHomingFlg = true;

        Vector2[] enemyPos = new Vector2[eight];
        GameObject[] shell = new GameObject[eight];

        for (int i = 0; i < eight; i++)
        {
            enemyPos[i] = transform.position;
            enemyPos[i].y += 1f;
            shell[i] = Instantiate(ShellPre, enemyPos[i], Quaternion.identity);
            sc = shell[i].GetComponent<ShellController>();
            sc.ec = GetComponent<EnemyController>();
        }
    }
    void EnemyDown()
    {
        DownFlg = true;
    }
    void EnemyDie()
    {
        
    }
}
