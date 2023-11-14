using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
    // public float spd = 10;
    public float dmg = 5;
    public float shock = 5;

    public int atkLayer = 0;
    // 0 ニュートラル
    // 1 Player
    // 2 Enemy

    [SerializeField, Header("生存時間")]
    float lifeTime = 2;

    private void FixedUpdate()
    {
        // this.transform.Translate(Vector3.right * spd * Time.deltaTime);

        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //if (collision.gameObject.tag == "Ground")
        //{
        //    Destroy(this.gameObject);
        //}
    }
}
