using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageTest : MonoBehaviour
{
    public static float speed = 5;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
         if (Input.GetKey(KeyCode.A))
        {
            transform.position -= speed * transform.right * Time.deltaTime;
        }
         if (Input.GetKey(KeyCode.D))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= speed * transform.up * Time.deltaTime*2;
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += speed * transform.up * Time.deltaTime*2;
        }
    }
}
