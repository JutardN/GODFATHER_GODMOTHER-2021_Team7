using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class intro : MonoBehaviour
{
    public GameObject fondNoir;
    public KeyCode pass;
    public Animator FadeText;
    public bool verif;
    void Start()
    {
        fondNoir.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(pass)&& verif == false)
        {
            StartCoroutine(FadeOut());
            verif = true;
        }
    }
    IEnumerator FadeOut()
    {
        FadeText.SetTrigger("FadeText");
        yield return new WaitForSeconds(1);
        fondNoir.SetActive(false);
    }
}
