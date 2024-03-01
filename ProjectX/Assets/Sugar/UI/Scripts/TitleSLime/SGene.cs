using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SGene : MonoBehaviour
{
    [SerializeField] GameObject Slime; //オリジナルのオブジェクト
    float time = 3f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        Vector3 Pos = this.transform.position;//生成したい位置を指定
        if (time<0)
        {
            Instantiate(Slime);
            time = Random.Range(3f,10f);
        }
    }
}
