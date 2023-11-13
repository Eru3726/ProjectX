using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollider : MonoBehaviour
{
    //[SerializeField, Header("MoveControllerをアタッチ")]
    //MoveController mc;

    [SerializeField, Header("親オブジェクトをいれてね")]
    GameObject parent;

    [SerializeField] float maxHp = 20;
    [SerializeField] float nowHp;

    [SerializeField] int hitLayer;

    // 無敵時間
    float invTime;

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
        if (invTime <= 0)
        {
            nowHp -= dmg;
            //mc.Flick(pos, -shock);
            invTime = 1;
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
        if (invTime > 0)
        {
            invTime -= Time.deltaTime;

            if (TryGetComponent(out Renderer rend))
            {
                rend.material.color = new Color32(255, 255, 255, 128);
            }
        }
        else
        {
            if (TryGetComponent(out Renderer rend))
            {
                rend.material.color = new Color32(255, 255, 255, 255);
            }
        }
    }

    public void SetInvTime(float time)
    {
        invTime = time;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<AttackCollider>(out AttackCollider atk))
        {
            if (atk.atkLayer != hitLayer)
            {
                Damage(atk.dmg, atk.transform.position, atk.shock);
                Debug.Log("hit: " + this.gameObject + " / " + this.nowHp);
            }
        }
    }

}
