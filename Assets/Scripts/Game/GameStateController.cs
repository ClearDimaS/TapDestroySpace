using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    #region Singleton

    public static GameStateController instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    #endregion

    [SerializeField]
    ResultsPanel resultsPanel;

    [SerializeField]
    GameOverlay gameOverlay;

    public Action OnGameOver;

    public void gameOver(bool ifWin) 
    {
        if(ifWin)
            LevelController.instance.changeLevelStatus(ifWin);

        OnGameOver?.Invoke();

        gameObject.SetActive(true);

        gameOverlay.ShowGameOverPanel();

        resultsPanel.gameOver(ifWin);

        Pause(true);
    }

    public void Pause(bool ifPause)
    {
        Time.timeScale = ifPause ? 0.0f : 1.0f;
    }

    public void LoadNextLevel() 
    {
        LevelController.instance.LoadNextLevel();
        Pause(false);
    }
}
