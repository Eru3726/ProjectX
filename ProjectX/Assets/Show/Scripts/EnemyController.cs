using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameObject player;
    Rigidbody2D rb;

    [SerializeField] EnemyAtk enemyAtk;

    public EnemyState currentState;
    public int Atkdis = 4;   

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
        yield return new WaitForSeconds(seconds);
        currentState = EnemyState.Warp;
    }

    public void EnemyMove()
    {
        Vector2 targetPos = player.transform.position;
        Vector2 enemyPos = transform.position;
        float x = targetPos.x;
        float y = 0f;
        Vector2 direction = new Vector2(x - transform.position.x, y).normalized;
        rb.velocity = direction * 3; //プレイヤーを追跡する処理
        float dis = Vector2.Distance(targetPos, enemyPos);

        if (dis <= Atkdis)
        {
            Debug.Log("追従");
            currentState = EnemyState.Dash;
        }
    }

    public void EnemyDash()
    {
        Debug.Log("突進");
        enemyAtk.EnemyAttack();
        
    }
}
