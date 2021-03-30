using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class SpawnManager : MonoBehaviour
{
    private readonly Vector3 _spawnPosition = new Vector3(30, 0, 0);
    public GameObject obstaclesPrefabs;
    private float startDelay = 2.0f;
    private float repeatRate = 2.0f;
    private PlayerController _playerControllerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        // Spawning the every repeat rate and after some delay
        InvokeRepeating(nameof(SpawnObstacle), startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        // Instantiating obstacle when it's not game over.
        
        if (_playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclesPrefabs, _spawnPosition, obstaclesPrefabs.transform.rotation); 
        }
    }
}
