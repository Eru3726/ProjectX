using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellController : MonoBehaviour
{
    public float ShellSpd = 5f;
    public float rotateSpd = 200f;

    float waittime = 0;

    Transform player;


    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    
    void Update()
    {
        waittime += Time.deltaTime;
        if(waittime >= 1f)
        {
            Debug.Log("s");
            Destroy(this.gameObject);
        }
    }
}
