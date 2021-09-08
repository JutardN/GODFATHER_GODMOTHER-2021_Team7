using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro : MonoBehaviour
{
    public GameObject fondNoir;
    public KeyCode pass;
    void Start()
    {
        fondNoir.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pass))
        {
            fondNoir.SetActive(false);
        }
    }
}
