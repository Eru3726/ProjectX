using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPlayer : MonoBehaviour ,IDamageable
{
    public int Health => hp;

    private int hp = 100;

    public void TakeDamage(int value)
    {
        Debug.Log("プレイヤーに" + value + "ダメージ");
        hp -= value;
        if(hp <= 0)
        {
            Debug.Log("プレイヤー死亡");
            Destroy(gameObject);
        }
    }
}
