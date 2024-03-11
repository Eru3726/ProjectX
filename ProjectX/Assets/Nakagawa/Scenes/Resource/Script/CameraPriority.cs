using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPriority : MonoBehaviour
{
    public GameObject Camera1;
    public GameObject Camera2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (CaveWarp.Camera == 1)
        {
            Camera1.SetActive(false);
            Camera2.SetActive(true);
        }
        if (CaveWarp.Camera == 0)
        {
            Camera1.SetActive(true);
            Camera2.SetActive(false);
        }
    }
}
