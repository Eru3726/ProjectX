using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    //[SerializeField, Header("MoveControllerをアタッチ")]
    //MoveController mc;

    [SerializeField] float maxHp = 20;
    float nowHp;

    [SerializeField] int hitLayer;

    // 無敵時間
    float invTimer;

    private void Start()
    {
        nowHp = maxHp;
    }

    private void FixedUpdate()
    {
        UpdateInv();
    }

    private void Damage(float dmg, Vector3 pos, float shock)
    {
        if (invTimer <= 0)
        {
            nowHp -= dmg;
            //mc.Flick(pos, -shock);
            invTimer = 2;
            //invTimer = shock * 0.1f;
        }
        else
        {
            Debug.Log("inv:" + this.gameObject);
        }

        if (nowHp <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("die:" + this.gameObject);
        Destroy(this.gameObject);
    }

    void UpdateInv()
    {
        if (invTimer >= 0)
        {
            invTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AtackCollider>(out AtackCollider atk))
        {
            if (atk.atkLayer != hitLayer)
            {
                Damage(atk.dmg, atk.transform.position, atk.shock);
                Debug.Log("hit: " + this.gameObject + " / " + this.nowHp);
                Destroy(atk.gameObject);
            }
        }
    }

}
