using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [Header("Canvas")]
    public GameObject CanvasGame;
    public GameObject CanvasRestart;

    [Header("CanvasRestart")]
    public GameObject WinTxt;
    public GameObject LoseTxt;

    [Header("Other")]
    public ScoreScript scoreScript;
    public PuckScript puckScript;
    public PlayerMovement playerMovement;
    public AiScript aiScript;
    public Countdown countdown;

    public void ShowRestartCanvas(bool didAiWin)
    {

        Time.timeScale = 0;

        CanvasGame.SetActive(false);
        CanvasRestart.SetActive(true);

        if (didAiWin)
        {
            WinTxt.SetActive(false);
            LoseTxt.SetActive(true);
        }
        else
        {
            WinTxt.SetActive(true);
            LoseTxt.SetActive(false);
        }

    }

    public void RestartGame()
    {

        Time.timeScale = 1;

        CanvasGame.SetActive(true);
        CanvasRestart.SetActive(false);

        scoreScript.ResetScores();
        puckScript.CenterPuck();
        playerMovement.ResetPosition();
        aiScript.ResetPosition();
        countdown.BeginCountdown();

    }

    public void ShowMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");

    }


}
