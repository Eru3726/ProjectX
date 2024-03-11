using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWarp : MonoBehaviour
{
    public static int Camera = 0;
    public GameObject Player;
    Vector3 Pltr;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Player")
        {
            
            Player.transform.position = new Vector3(-6, -3);
            Camera = 1;
        }
    }
}
