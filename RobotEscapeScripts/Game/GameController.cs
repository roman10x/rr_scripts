using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

using ThirteenPixels.Soda;

public enum GameState
{
    INIT,
    STARTED
}
public class GameController : MonoBehaviour
{
    [SerializeField] GlobalGameState gameState;
    [SerializeField] private GlobalEnemySpawner enemySpawner;
    private void Start()
    {
        enemySpawner.componentCache.SpawnEnemies();
        gameState.value = GameState.STARTED;
    }

    /// <summary>
    /// Launch countdown and restarts the game
    /// </summary>
    public void Restart()
    {
        gameState.value = GameState.INIT;
        SceneManager.LoadScene(0);
    }
}
