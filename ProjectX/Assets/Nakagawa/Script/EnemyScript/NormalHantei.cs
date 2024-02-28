using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalHantei : MonoBehaviour
{

    public bool jud = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        if (tag == "Player")
        {
            jud = true;
        }
    }

    public bool Getjud
    {
        get
        {
            return jud;
        }
    }
}
