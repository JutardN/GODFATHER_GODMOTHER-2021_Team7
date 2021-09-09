using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public KeyCode starter;
    public KeyCode ender;

    public bool isStart;
    public bool isEnd;

    public Animator fade;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(starter)&& isStart == false)
        {
            fade.SetTrigger("Start");
            isStart = true;
            isEnd = false;
        }
        if (Input.GetKeyDown(ender)&& isEnd == false)
        {
            fade.SetTrigger("End");
            isEnd = true;
            isStart = false;
        }
    }
}
