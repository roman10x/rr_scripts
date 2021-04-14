using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ThirteenPixels.Soda;

public class Enemy : Entity
{
    [SerializeField] protected GlobalTransform player;
    [SerializeField] protected GlobalEnemyHandler enemyHandler;
    [SerializeField] protected GameEvent onPlayerDeath;
    [SerializeField] protected float movingTime;  
    [SerializeField] protected float waitingTime; 
    [SerializeField] protected float randomTime;  // Random time to add to moving time
    [SerializeField] protected float touchDamageMultiplier = 1; 
    [SerializeField] protected int coinsToDrop = 100; 
    protected Player touchingPlayer; 
    protected void OnEnable()
    {
        onPlayerDeath.onRaise.AddResponse(ResetTouchingPlayer);
    }
    protected void OnDisable()
    {
        onPlayerDeath.onRaise.RemoveResponse(ResetTouchingPlayer);
    }

    private void ResetTouchingPlayer()
    {
        touchingPlayer = null;
    }

    protected override void Death(Entity killer)
    {
        Player player = killer as Player;
        if(player!=null)
            player.AddCoins(coinsToDrop);
        enemyHandler.componentCache.RemoveEnemy(this);
        Destroy(gameObject);
    }

    protected void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Player>();
        if (player != null)
        {
            touchingPlayer = player;
            player.TakeDamage(new DamageReport(damage * touchDamageMultiplier, this));
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == Tags.playerTag)
            touchingPlayer = null;
    }
}
