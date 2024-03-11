using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaveWarp2 : MonoBehaviour
{
    public GameObject Player;
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
        if (collision.tag == "Player")
        {

            Player.transform.position = new Vector3(101, -3);
            CaveWarp.Camera=0;
        }
    }
}
