using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalCon : MonoBehaviour
{
    NormalHantei normalhantei;

    // Start is called before the first frame update
    void Start()
    {
        normalhantei = GetComponent<NormalHantei>();//範囲内に入ったら
    }

    // Update is called once per frame
    void Update()
    {
        if (normalhantei.Getjud)
        {
            
        }
    }
    
}
