using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody _playerRb;
    private Animator _playerAnim;
    private AudioSource _playerAudio;
    private AudioSource _mainMusic;
    private Camera _camera;
    private float jumpForce = 900f;
    private float gravityModifier = 3f;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private bool isJumping;
    public bool gameOver = false;
    private static readonly int DeathB = Animator.StringToHash("Death_b");
    private static readonly int DeathTypeINT = Animator.StringToHash("DeathType_int");
    private static readonly int JumpTrig = Animator.StringToHash("Jump_trig");

    private void Awake()
    {
        // reset gravity
        Physics.gravity = new Vector3(0, -9.8F, 0);
        Physics.gravity *= gravityModifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        _playerRb = GetComponent<Rigidbody>();
        _playerAnim = GetComponent<Animator>();
        _playerAudio = GetComponent<AudioSource>();
        _camera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        _mainMusic = _camera.GetComponent<AudioSource>();
        
        isJumping = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        // Jumping if not game over and player on the ground
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && !isJumping)
        {
            Jumping();
        }
    }

    private void Jumping()
    {
        _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _playerAudio.PlayOneShot(jumpSound, 1.0f);
        //isOnGround = false;
        isJumping = true;
        dirtParticle.Stop();
        // Starting jump animation
        _playerAnim.SetTrigger(JumpTrig);

    }

    private void OnCollisionEnter(Collision collision)
    {
        // Checking if player is on on ground. And turning on dirt animation.
        if (collision.gameObject.CompareTag("Ground") && !gameOver)
        {
           // isOnGround = true;
            isJumping = false;
            dirtParticle.Play();
        }
        
        // if player hit the obstacle - game over.
        else if (collision.gameObject.CompareTag("Obstacle") && transform.position.y < 1.4)
        {
            gameOver = true;
            _playerAnim.SetBool(DeathB, true);
            _playerAnim.SetInteger(DeathTypeINT, 1);
            _playerAudio.PlayOneShot(crashSound, 1.0f);
            explosionParticle.Play();
            _mainMusic.Stop();
            dirtParticle.Stop();
        }
    }
}
