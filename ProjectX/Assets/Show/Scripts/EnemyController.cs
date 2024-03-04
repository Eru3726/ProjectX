using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] GameObject Guren_GS;

    [SerializeField] Animator GurenAnim;

    GameObject player;

    MoveController mc;

    MoveController plmc;

    ShellController sc;

    Rigidbody2D rb;

    [Header("Enemyの挙動")]
    public EnemyState currentState;
    [Header("Playerとの距離")]
    public int Atkdis = 4;
    [Header("Warpのbool型変数")]
    public bool movecheck = true;
    [Header("Moveのbool型変数")]
    public bool warpcheck = true;

    public bool nowDashFlg = false;

    public float minX = 3f;  // 移動可能なX座標の最小値
    public float maxX = 6f;   // 移動可能なX座標の最大値
    public float minY = 4f;   // 移動可能なY座標の最小値
    public float maxY = 6f;    // 移動可能なY座標の最大値

    float timer = 0;

    [Header("Enemyの弾Prefab")]
    public GameObject ShellPre;

    int eight = 8;
    int Movecounter = 0;
    int Homcounter = 0;

    float warpDelay = 2f; //ワープするまでの時間
    float idolDelay = 1f; //待機時間
    float Movetimer = 0f;
    float Homtimer = 0f;
    float distance = 0.1f;

    Mng_Game mg;



    bool animeHomFlg = false;
    bool animeMoveFlg = false;
    bool DieFlg = false;


    private bool nowHomingFlg = false;
    public bool[] ishoming = new bool[8];


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
        GurenAnim = Guren_GS.GetComponent<Animator>();
        ishoming = new bool[8];
        for (int i = 0; i < ishoming.Length; i++) ishoming[i] = false;
        mg = GameObject.Find("GameManager").GetComponent<Mng_Game>();
    }

    void FixedUpdate()
    {
        if (!DieFlg)
        {
            Movetimer += Time.deltaTime;

            Vector3 dis = player.transform.position - transform.position;

            if (dis.x >= -distance)
            {
                transform.localScale = new Vector3(-2, 2, 1);
            }
            if (dis.x <= distance)
            {
                transform.localScale = new Vector3(2, 2, 1);
            }

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

                case EnemyState.Homing: //ホーミング攻撃の処理

                    StartCoroutine(HomDelay());
                    Debug.Log("Homing");
                    break;

                case EnemyState.Dash: //突進の処理

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

                case EnemyState.Down: //ダウン


                    break;
                case EnemyState.Die: //死んだ後の処理
                    break;
            }
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
        //this.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        yield return new WaitForSeconds(warpDelay);
        EnemyWarp();
    }

    IEnumerator HomDelay()
    {
        yield return new WaitForSeconds(2);
        EnemyHoming();

    }

    IEnumerator ShiftDelay()
    {
        yield return new WaitForSeconds(1f);
    }
    void EnemyMove()
    {
        //if (movecheck && plmc.IsGround())
        //{
        //    GurenAnim.Play("Guren_FSAnimation");
        //    Vector2 playerPos = player.transform.position;
        //    Vector2 enemyPos = transform.position;
        //    float directionX = playerPos.x - transform.position.x;

        //    mc.InputLR((int)Mathf.Sign(directionX));

        //    float dis = Vector2.Distance(playerPos, enemyPos);

        //    if (dis <= Atkdis)
        //    {
        //        mc.InputLR(0);
        if (!animeMoveFlg)
        {
            if (Movetimer >= 2)
            {
                animeMoveFlg = true;
                GurenAnim.Play("Guren_FSAnimation");
            }
        }
        else
        {
            if (Movetimer >= 4)
            {
                currentState = EnemyState.Dash;
            }
        }

        //    }

        //}
        //else
        //{
        //    GurenAnim.Play("Guren_NomalAnimation");
        //}
    }

    void EnemyDash()
    {
        Debug.Log("突進");
        Vector3 pos = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
        mc.InputFlick(pos, 35, 0.3f, true);
        currentState = EnemyState.Idol;
    }

    void EnemyWarp()
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
    void EnemyHoming()
    {
        GurenAnim.Play("Guren_FingerSnapOnlyAnimation");

        if (ishoming.All(b => b))
        {
            Debug.Log("動いた");
            Movetimer = 0;
            animeMoveFlg = false;
            currentState = EnemyState.Move;
            nowHomingFlg = false;
            for (int i = 0; i < ishoming.Length; i++) ishoming[i] = false;
            return;
        }
        if (nowHomingFlg) return;

        nowHomingFlg = true;


        //Vector2[] enemyPos = new Vector2[eight];
        GameObject[] shell = new GameObject[eight];

        Vector2 enemyPos = transform.position;
        enemyPos.x += 1.0f;

        for (int i = 0; i < eight; i++)
        {
            Debug.Log("a");
            //enemyPos[i] = transform.position;
            enemyPos.y += 0.4f;
            //transform.position = enemyPos[i];
            shell[i] = Instantiate(ShellPre, enemyPos, Quaternion.identity);
            StartCoroutine(ShiftDelay());
            Debug.Log(shell[i]);
            sc = shell[i].GetComponent<ShellController>();
            sc.ec = GetComponent<EnemyController>();
            sc.num = i;
        }
    }
    public void EnemyDown()
    {
        GurenAnim.Play("Guren_GS_DownAnimation");
    }
    public void EnemyDie()
    {
        //死亡処理
        GurenAnim.Play("Guren_FingerSnapOnlyAnimation");
        mg.OneShotSE_C(SEData.Type.EnemySE, Mng_Game.ClipSe.Death);
        DieFlg = true;
        Destroy(gameObject,4.0f);
    }
}
