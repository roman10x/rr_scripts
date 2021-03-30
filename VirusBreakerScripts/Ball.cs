using UnityEngine;



public class Ball : MonoBehaviour
{
    [SerializeField] private Platform plaform1;
    [SerializeField] private float xPush = -2f;
    [SerializeField] private float yPush = 9f;
    [SerializeField] private float randomFactor = 0.5f;
    
    private bool hasStarted;
    private Vector2 platformToBallVector;
    private AudioSource ballAudioSource;
    private Rigidbody2D ballRb2D;
    private GameSession _gameSession;

    
    // Start is called before the first frame update
    void Start()
    {
        ballAudioSource = GetComponent<AudioSource>();
        ballRb2D = GetComponent<Rigidbody2D>();
        _gameSession = FindObjectOfType<GameSession>();
        
        platformToBallVector = transform.position - plaform1.transform.position;
        hasStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPlatform(); 
            LaunchBallOnMouseClick();
            
        }
        
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0) || _gameSession.IsAutoPlayEnabled())
        {
            hasStarted = true;
            ballRb2D.velocity = new Vector2(xPush, yPush);
        }
    }
    private void LockBallToPlatform()
    {
        // Placing ball to platform
        Vector2 platfromPosition = new Vector2(plaform1.transform.position.x, plaform1.transform.position.y);
        transform.position = platfromPosition + platformToBallVector;
        
    }

    private void OnCollisionEnter2D(Collision2D colission)
    {
        Vector2 velocityTweak = new Vector2
            (Random.Range(0.2f, randomFactor), 
            Random.Range(0.2f, randomFactor));
        
        if (hasStarted)
        {
            ballAudioSource.Play();
            // adding random tweak to velocity to avoid sticking of the ball in infinite loop of jumping.  
            ballRb2D.velocity += velocityTweak;
        }        
    }
}
