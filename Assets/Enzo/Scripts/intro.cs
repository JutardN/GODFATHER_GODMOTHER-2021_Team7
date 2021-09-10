using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Rewired;

public class intro : MonoBehaviour
{
    public GameObject fondNoir;
    public KeyCode pass;
    public Animator FadeText;
    public bool verif;

    private GameManager manager;
    private Player player;

    void Start()
    {
        fondNoir.SetActive(true);
        manager = GameManager.Instance;
        player = manager.player.playerController;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetButton("Action")&& verif == false)
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
