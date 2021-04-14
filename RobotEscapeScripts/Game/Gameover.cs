using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using ThirteenPixels.Soda;
using UnityEngine.SceneManagement;

public class Gameover : MonoBehaviour
{
    [SerializeField] GlobalGameState gameState;
    [SerializeField] GameEvent onPlayerDeath;
    [SerializeField] TextMeshProUGUI gameOverText;
    [SerializeField] GameObject canvas;
    private bool isGameOver = false;

    private void OnEnable()
    {
        onPlayerDeath.onRaise.AddResponse(ShowGameOverScreen);
    }

    private void OnDisable()
    {
        onPlayerDeath.onRaise.RemoveResponse(ShowGameOverScreen);
    }

    private void ShowGameOverScreen()
    {
        gameOverText.text = "GAME OVER".ToString();
        isGameOver = true;
        canvas.SetActive(true);
    }
   

    public void RestartScene()
    {
        gameState.value = GameState.INIT;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
