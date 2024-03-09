using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    [SerializeField] GameObject Guren_GS;

    [SerializeField] Animator GurenAnim;

    [SerializeField] story_stage2_Koya koya;

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
    float[] probabi;

    [Header("Gurenの弾Prefab")]
    public GameObject ShellPre;

    [Header("Gurenの死亡エフェクトPrefab")]
    public GameObject DieEffectPre;

    int ten = 10;
    int eight = 8;
    int seven = 7;
    int six = 6;
    int five = 5; 
    int Homcounter = 0;
    int sponcounter = 3;


    float warpDelay = 2f; //ワープするまでの時間
    float idolDelay = 1f; //待機時間
    float homdelay = 1.5f;
    float diedelay = 1f;
    float Movetimer = 0f;
    float Homtimer = 0f;
    float distance = 0.1f;

    Mng_Game mg;

    bool animeHomFlg = false;
    bool animeMoveFlg = false;
    bool DieFlg = false;


    private bool nowHomingFlg = false;
    private bool halfHp = false;
    public bool[] ishoming = new bool[8];


    public enum EnemyState
    {
        Standby,
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
        currentState = EnemyState.Standby;
        mc = gameObject.GetComponent<MoveController>();
        plmc = player.gameObject.GetComponent<MoveController>();
        sc = gameObject.GetComponent<ShellController>();
        GurenAnim = Guren_GS.GetComponent<Animator>();
        ishoming = new bool[8];
        for (int i = 0; i < ishoming.Length; i++) ishoming[i] = false;
        mg = GameObject.Find("GameManager").GetComponent<Mng_Game>();
        koya = gameObject.GetComponent<story_stage2_Koya>();
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            EnemyDie();
        }
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
                case EnemyState.Standby:
                    Standby();
                    break;
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

                    //StartCoroutine(HomDelay());
                    StartCoroutine(HomAnimationPlay());
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
    }

    IEnumerator ShiftDelay()
    {
        yield return new WaitForSeconds(1f);
    }

    IEnumerator HomAnimationPlay()
    {
        GurenAnim.Play("Guren_FingerSnapOnlyAnimation");

        yield return new WaitForSeconds(GurenAnim.GetCurrentAnimatorStateInfo(0).length);

        yield return new WaitForSeconds(homdelay);

        EnemyHoming();
    }
    IEnumerator DieAnimationPlay()
    {
        GurenAnim.Play("Guren_GS_DownAnimation");

        yield return new WaitForSeconds(diedelay);

        DieFlg = true;
    }

    IEnumerator SponAction()
    {
        Vector2 EnemyPos = transform.position;
        GurenAnim.Play("Guren_GS_DownAnimation");
        DieFlg = true;
        for(int i = 0; i < sponcounter; i++)
        {
            Instantiate(DieEffectPre, EnemyPos, Quaternion.identity);
            yield return new WaitForSeconds(diedelay);
        }
        koya.FinishBattle();
        if (koya.FinishText())
        {

        }
    }

    void Standby()
    {
        currentState = EnemyState.Move; /*(EnemyState)Enum.ToObject(typeof(EnemyState), RandomAction());*/
    }
    void EnemyMove()
    {
        
        if (!animeMoveFlg)
        {
            if (Movetimer >= five)
            {
                animeMoveFlg = true;
                GurenAnim.Play("Guren_FSAnimation");
            }
        }
        else
        {
            if (Movetimer >= seven)
            {
                currentState = EnemyState.Dash;
            }
        }
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

            float randomX = UnityEngine.Random.Range(minX, maxX);
            float randomY = UnityEngine.Random.Range(minY, maxY);
            transform.position = new Vector3(randomX, randomY, 0);

            warpcheck = false;
        }
        currentState = EnemyState.Homing;
    }
    void EnemyHoming()
    {
        if (!DieFlg)
        {
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
            enemyPos.x += 0.2f;

            for (int i = 0; i < eight; i++)
            {
                Debug.Log("a");
                //enemyPos[i] = transform.position;
                enemyPos.y += 0.15f;
                //transform.position = enemyPos[i];
                shell[i] = Instantiate(ShellPre, enemyPos, Quaternion.identity);
                StartCoroutine(ShiftDelay());
                Debug.Log(shell[i]);
                sc = shell[i].GetComponent<ShellController>();
                sc.ec = GetComponent<EnemyController>();
                sc.num = i;
            }
            Movetimer = 0;
        }
    }
    public void EnemyDown()
    {
        StartCoroutine(SponAction());
    }
    public void EnemyDie()
    {
        //死亡処理
        //if() 大聖から貰う
        Debug.Log("死んだ");

    }
       

    private int RandomAction()
    {
        if (halfHp) probabi = new float[] { 0.60f, 0.25f, 0.15f };
        else probabi = new float[] { 0.60f, 0.30f, 0.00f };
        float rand = UnityEngine.Random.value;
        float cumuprobabi = 0f;
        for (int i = 0; i <= probabi.Length; i++)
        {
            cumuprobabi += probabi[i];
            if(rand < cumuprobabi)
            {
                return i + 3;
            }
        }
        return probabi.Length;
    }
}
