using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Shell
{
    protected override void OnEnemyCollision(Entity entity) 
    {
        entity.TakeDamage(damageReport);
        shooter.DeactivateShell(gameObject);
        
    }

    protected override void OnObstacleCollision(Transform obstacle)
    {
        shooter.DeactivateShell(gameObject);
    }

    protected override void OnPlayerCollision(Entity entity)
    {
        
    }
}
