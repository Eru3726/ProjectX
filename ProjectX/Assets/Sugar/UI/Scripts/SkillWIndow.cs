using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWIndow : MonoBehaviour
{
    [SerializeField]GameObject MenuWindow;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        InputKEY();
    }
    void InputKEY()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            MenuWindow.SetActive(true);
            MenuWindow.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0,0);
            this.gameObject.SetActive(false);
        }
    }
}
