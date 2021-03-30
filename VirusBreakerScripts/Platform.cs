using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] float screenWidthInUnits = 16f;

    [SerializeField] private float platformMinX = .5f;
    [SerializeField] private float platformMaxX = 15.5f;

    private GameSession _gameSession;
    private Ball _ball;

    private void Start()
    {
        _gameSession = FindObjectOfType<GameSession>();
        _ball = FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        // Moving platform based on mouse position, with limits on left and right
        Vector2 platfromPosition = new Vector2(transform.position.x, transform.position.y);
        platfromPosition.x = Mathf.Clamp(GetXPos(), platformMinX, platformMaxX);
        transform.position = platfromPosition;
    }
    
    private float GetXPos()
    {
        if (_gameSession.IsAutoPlayEnabled())
        {
            return _ball.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;
        }
    }
}
