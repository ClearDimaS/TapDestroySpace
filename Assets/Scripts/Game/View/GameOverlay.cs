using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverlay : MonoBehaviour
{
    [SerializeField]
    GameObject PausePanel;

    [SerializeField]
    GameObject GameOverPanel;

    [SerializeField]
    GameStateController gameStateController;

    private void OnDisable()
    {
        HideUserPanels();
    }

    public void PausePressed()
    {
        if (GameOverPanel.activeSelf == false) 
        {
            PausePanel.SetActive(true);
            gameStateController.Pause(true);
        }
    }

    private void HideUserPanels()
    {
        PausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
    }

    public void ExitGame() 
    {
        gameStateController.OnGameOver();
        gameStateController.Pause(false);
        UI_Controller.Instance.SetActivePanel(0);
    }

    public void ContinueGame() 
    {
        gameStateController.Pause(false);
        HideUserPanels();
    }

    public void ShowGameOverPanel() 
    {
        GameOverPanel.SetActive(true);
    }
}
