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
    }

    void FixedUpdate()
    {
        switch(currentState)
        {

            case EnemyState.Idol:　//次の行動に移るための待機

                StartCoroutine(WaitForSomeTime(3f));
                break;

            case EnemyState.Move: //ワープの処理

                EnemyMove(); //Enemyの通常時の動き

                break;

            case EnemyState.Homing:　//ホーミング攻撃の処理


                break;

            case EnemyState.Dash:　//突進の処理

                EnemyDash();
                
                break;

            case EnemyState.Warp:  //移動

                
    
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
        currentState = EnemyState.Warp;
    }

    public void EnemyMove()
    {
        if (tracking)
        {
            Vector2 playerPos = player.transform.position;
            Vector2 enemyPos = transform.position;
            Vector2 pos = new Vector2(playerPos.x, 0);
            Vector2 direction = new Vector2(pos.x - transform.position.x, pos.y).normalized;
            rb.velocity = direction * 3; //プレイヤーを追跡する処理
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
        mc.InputFlick(Vector3.left, 50, 1,false);

        mc.InputLR(-1);
    }
}
