using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSession : MonoBehaviour
{
    // Setting up slider to control game speed
    [Range(0.5f,10f)] [SerializeField] private float gameSpeed = 1f;
    [SerializeField] int pointsPerBlockDestroyed = 83;
    

    [SerializeField] private int currentScore;
    [SerializeField] bool isAutoPlayEnabled;
    
    private TextMeshProUGUI scoreText;

    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameSession>().Length;
        if (gameStatusCount > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        scoreText = GameObject.Find("ScoreTextField").GetComponent<TMPro.TextMeshProUGUI>();
        scoreText.text = currentScore.ToString();
    }

    public void AddToScore()
    {
        currentScore += pointsPerBlockDestroyed;
        scoreText.text = currentScore.ToString();
    }

    public void ResetGame()
    {
        Destroy(gameObject);  
    }

    public bool IsAutoPlayEnabled()
    {
        return isAutoPlayEnabled;
    }
}
