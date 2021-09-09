using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string SceneToLoad_Play;
    public Animator transition;

    public void Play()
    {
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(SceneToLoad_Play);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("ca quitte a partir du menu principal");
    }
}
