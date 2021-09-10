using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;

public class PauseMenu : MonoBehaviour
{
    public KeyCode keyPause;
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private GameManager manager;
    private Player playerController;
    private bool pause;

    private void Start()
    {
        manager = GameManager.Instance;
        playerController = manager.player.playerController;
    }

    void Update()
    {
        if (playerController.GetButton("Pause") && !pause)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
            StartCoroutine("WaitBetweenPause");
        }
    }

    IEnumerator WaitBetweenPause()
    {
        pause = true;
        yield return new WaitForSecondsRealtime(0.2f);
        pause = false;
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("ça quitte à partir du menu pause");
    }
}
