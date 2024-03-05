using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackOption : MonoBehaviour
{

    [SerializeField, Header("地形に当たったらDestroy")]
    protected bool groundDestroyFlag = false;

    [SerializeField, Header("Groundをアタッチ")]
    LayerMask groundLm;

    [SerializeField, Header("生存時間オンオフ")]
    protected bool isLife = false;

    [SerializeField, Header("生存時間")]
    public float lifeCount = 2;

    [SerializeField, Header("攻撃が当たったら消えるか")]
    public bool isBullet = false;


    private void FixedUpdate()
    {
        lifeCount -= Time.deltaTime;
        if (lifeCount <= 0)
        {
            Destroy(this.gameObject);
        }

        if (groundDestroyFlag)
        {
            float rayLen = 1;
            Vector3 startPos = this.transform.position;
            float nowRot = Mathf.Repeat(this.transform.localEulerAngles.z + 180, 360) + 180;
            Vector3 rayPos = new Vector3(rayLen * Mathf.Cos(nowRot * Mathf.Deg2Rad), rayLen * Mathf.Sin(nowRot * Mathf.Deg2Rad), 0);
            RaycastHit2D result;
            result = Physics2D.Linecast(startPos, startPos + rayPos, groundLm);
            Debug.DrawLine(startPos, startPos + rayPos, color: Color.blue);

            if (result.collider != null)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag(this.gameObject.tag.ToString()) && isBullet)
        {
            if (collision.TryGetComponent<IDamageable>(out IDamageable id))
            {
                Destroy(this.gameObject);
            }
        }
    }
}
