using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayer : MonoBehaviour ,IDamageable
{
    public int Health => hp;

    private int hp = 100;

    public void TakeDamage(int damage, float shock)
    {
        Debug.Log("プレイヤーに" + damage + "ダメージ");
        hp -= damage;
        if(hp <= 0)
        {
            Debug.Log("プレイヤー死亡");
            Destroy(gameObject);
        }
    }
}
