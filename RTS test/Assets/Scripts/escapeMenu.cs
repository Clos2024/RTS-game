using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escapeMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenu;
    public GameObject selectionBox,unitSelections;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
        selectionBox.SetActive(true);
        unitSelections.SetActive(true);
    }
    void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        selectionBox.SetActive(false);
        unitSelections.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
