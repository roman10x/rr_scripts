using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    [SerializeField] GlobalGameState gameState;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == Tags.playerTag)
        {
            LoadNextLevel();
        }
    }
    
    public void LoadNextLevel()
    {
        gameState.value = GameState.INIT;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    
    public void LoadStartLevel()
    {
        gameState.value = GameState.INIT;
        SceneManager.LoadScene(0);
    }
}
