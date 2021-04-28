using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : SingletonBehaviour<GameManager>
{
    [SerializeField] private Board board = null;
    [SerializeField] private int movesAvailable = 20;
    
    
    // The score of the player
    public static int Score { get; private set; }
    
    public int MovesAvailable
    {
        get => movesAvailable;
        set => movesAvailable = value;
    }
    
    private void OnEnable()
    {
        GameEvents.OnElementsDespawned.AddListener(OnElementsDespawned);
    }
    
    private void OnDisable()
    {
        GameEvents.OnElementsDespawned.RemoveListener(OnElementsDespawned);
    }
    
    private IEnumerator Start()
    {
        
        
        InitializeGame();

        //Wait for the game to be executed completely
        yield return StartCoroutine(RunGame());

        //Wait for the game to finish
        yield return StartCoroutine(EndGame());
    }
    
    
   
    public void InitializeGame()
    {
        Score = 0;
        board.CreateBoard();
    }
    
    // Runs the game loop
    public IEnumerator RunGame()
    {
        //Game Loop
        while (MovesAvailable > 0)
        {
            //Wait for the Player to select elements
            yield return board.WaitForSelection();

            //Despawn selected elements
            yield return board.DespawnSelection();

            //Wait for the grid elements to finish movement
            yield return board.WaitForMovement();

            //Respawn despawned elements
            yield return board.RespawnElements();
        }
    }
    
    public IEnumerator EndGame()
    {
        yield return new WaitForSeconds(0.5f);

        SceneManager.LoadScene(2);
    }
    private void OnElementsDespawned(int count)
    {
        //Update score
        int oldScore = Score;
        Score = oldScore + count * (count - 1);

        //Invoke score changed event
        GameEvents.OnScoreChanged.Invoke(oldScore, Score);
    }
}
