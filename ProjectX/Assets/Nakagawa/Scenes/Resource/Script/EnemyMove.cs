using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rb;
    Vector3 Flypos;
    float timer = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Vector3 Flypos = rb.position;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer <1)
        {
            Move1();
        }
        else 
        {
            Move2();
             if (timer > 2)
            {
                timer = 0;
            }
        }
        
      
        
    }
    // 上下動作
    void Move1()
    {
       
        Flypos += new Vector3(0.0f, 0.02f, 0.0f);
        rb.position = Flypos;
    }
    void Move2()
    {
        Flypos += new Vector3(0.0f, -0.02f, 0.0f);
        rb.position = Flypos;
    }
}
