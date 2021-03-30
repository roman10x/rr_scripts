using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config parameters
    [SerializeField] private AudioClip explodingSound;
    [SerializeField] private GameObject blockSparklesVFX;
    [SerializeField] private Sprite[] hitSprites;
    
    // cached reference
    private Level level;
    private GameSession _gameSession;
    
    // state variables
    [SerializeField] private int timesHit;

    private void Start()
    {
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        _gameSession = FindObjectOfType<GameSession>();
        if (tag == "Breakable")
        {
            level.CountBlocks();
        }
        
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball" && tag == "Breakable")
        {
            HandleHit();
        }
        
    }
    
    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array " + gameObject.name);
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();            
        }
    }

    private void DestroyBlock()
    {
        _gameSession.AddToScore();
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.CountDestroyedBlocks();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        AudioSource.PlayClipAtPoint(explodingSound, new Vector3(8, 8, -8));
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }
}
