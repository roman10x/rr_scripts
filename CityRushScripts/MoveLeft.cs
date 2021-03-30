using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    // setting up moving speed
    private float speed = 20f;
    private PlayerController _playerControllerScript;
    private float leftBound = -15;
    
    // Start is called before the first frame update
    void Start()
    {
        _playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moving back ground if game over is false
        if (!_playerControllerScript.gameOver)
        {
            transform.Translate(Vector3.left * (Time.deltaTime * speed));   
        }
        
        // Stop moving when player hit obstacle
        if (transform.position.x < leftBound && gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }
}
