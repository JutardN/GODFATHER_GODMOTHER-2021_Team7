using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public string SceneToLoad_Play;

    public void Play()
    {
        SceneManager.LoadScene(SceneToLoad_Play);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("�a quitte � partir du menu principal");
    }
}
