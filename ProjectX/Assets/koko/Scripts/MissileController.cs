using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    StageData.LAYER_DATA targetLayer = StageData.LAYER_DATA.Enemy;

    [SerializeField]
    LayerMask targetLm;

    [SerializeField]
    float spd = 10;

    [SerializeField]
    float rotSpd = 10;

    [SerializeField]
    public float delayCount = 1;

    [SerializeField]
    float rayLen = 5;

    [SerializeField]
    float rayRot = 60;

    [SerializeField]
    float rayValue = 5;

    

    

    private void FixedUpdate()
    {
        // カウントが0になったら起動
        if (delayCount > 0)
        {
            delayCount -= Time.deltaTime;
        }
        else
        {
            // 索敵
            if (target == null)
            {
                Vector3 startPos = this.transform.position;
                float partRot = rayRot / (rayValue - 1);
                float nowRot = Mathf.Repeat(this.transform.localEulerAngles.z + 180, 360) - 180 + (rayRot / 2);

                for (int i = 0; i < rayValue; i++)
                {
                    Vector3 rayPos = new Vector3(rayLen * Mathf.Cos(nowRot * Mathf.Deg2Rad), rayLen * Mathf.Sin(nowRot * Mathf.Deg2Rad), 0);
                    RaycastHit2D result;
                    result = Physics2D.Linecast(startPos, startPos + rayPos, targetLm);
                    Debug.DrawLine(startPos, startPos + rayPos, color: Color.red);

                    if (result.collider != null)
                    {
                        // Debug.Log(result.collider);
                        var hc = result.collider.GetComponent<OldHitCollider>();
                        if (hc != null)
                        {
                            if (hc.GetHitLayer() == targetLayer)
                            {
                                target = result.collider.gameObject;
                            }
                        }
                    }
                    nowRot -= partRot;
                }
            }
            else // 対象を追跡
            {
                Vector3 direction = target.transform.position - this.transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, rotSpd * Time.deltaTime);
            }

            // 移動
            this.transform.Translate(Vector3.right * spd * Time.deltaTime);
        }


    }
}
