using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentScoreText = null;
    [SerializeField] private TextMeshProUGUI topScoreText = null;
    

    private void Start()
    {
        currentScoreText.text = GameManager.Score.ToString();
        
        
        //Check if the player has reached a new highscore
        if (GameManager.Score > HighscoreManager.Instance.Highscore)
        {
            HighscoreManager.Instance.Highscore = GameManager.Score;
            
        }
        
        topScoreText.text = HighscoreManager.Instance.Highscore.ToString();
    }
}
