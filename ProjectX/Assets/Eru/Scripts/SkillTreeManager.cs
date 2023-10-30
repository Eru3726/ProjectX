using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTreeManager : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void Update()
    {
        //var scroll = Input.mouseScrollDelta.y * Time.deltaTime * 10;
        //Debug.Log(scroll);

        //if (mainCam.orthographicSize > 0) mainCam.orthographicSize += scroll;
        //else mainCam.orthographicSize -= scroll;
        //mainCam.orthographicSize = Mathf.Clamp(mainCam.orthographicSize, 5, 20);
    }
}
