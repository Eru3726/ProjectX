using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeCon : MonoBehaviour
{
    public float deleteTime = 10.0f;
    [SerializeField] float Under; 
    void Start()
    {
        Destroy(gameObject, deleteTime);
    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;

        // 座標を取得
        Vector3 pos = myTransform.position;
       // pos.x += 0.01f;    // x座標へ0.01加算
        pos.y -= 0.02f*Under;    // y座標へ0.01加算
       // pos.z += 0.01f;    // z座標へ0.01加算
        myTransform.position = pos;  // 座標を設定
    }
}
