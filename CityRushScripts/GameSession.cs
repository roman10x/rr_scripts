using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI uiTimer;
    [SerializeField] private TextMeshProUGUI gameText;
    [SerializeField] private Image gameOverShadow;
    //float timeToStartFading = 0f;
    float fadeSpeed = .15f;
    
    private float timer;
    private bool gameOver;
    private PlayerController _playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
        gameText.text = "PRESS SPACE TO JUMP".ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            gameText.text = " ".ToString();
        }
        
        if (!gameOver)
        {
            TimerCounting();
        }
        else
        {
            gameText.text = "GAME OVER!\n" +
                                "press space to start running again".ToString();
            
            FadeOutScreen();
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            
        }
        
    }

    private void TimerCounting()
    {
        gameOver = _playerController.gameOver;
        timer += Time.deltaTime;
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer % 60F);
        int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
        uiTimer.text = minutes.ToString("00") +
                       ":" + seconds.ToString("00") +
                       ":" + milliseconds.ToString("00");
    }

    void FadeOutScreen()
    {
        gameOverShadow.color = new Color(gameOverShadow.color.r, 
            gameOverShadow.color.g, 
            gameOverShadow.color.b, 
            gameOverShadow.color.a + (fadeSpeed * Time.deltaTime));
        
    }
   
    
}
