using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private const float overlapSphereRadius = 0.5f;
    [Header("Spawn settings")]
    [SerializeField] private int enemyCount;
    [SerializeField] private List<GameObject> enemies;
    private EnemyHandler enemyHandler;

    private void Awake()
    {
        if(enemyHandler==null)
            enemyHandler = GetComponent<EnemyHandler>();
    }

    public void SpawnEnemies() 
    {
        if (enemies.Count == 0)
            throw new System.ArgumentNullException("enemies", "Enemies list is empty");
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPos;
            int loopBreaker = 0;
            do
            {
                loopBreaker++;
                spawnPos = new Vector3(Random.Range(-1.4f,1.4f), 1f, Random.Range(-1.2f, 1.2f));
            } while (CheckCollisions(spawnPos) && loopBreaker<100); 
            
            if (loopBreaker < 100)
            {
                var newEnemy = Instantiate(enemies[Random.Range(0, enemies.Count)], transform);
                newEnemy.transform.position = spawnPos;
                enemyHandler.AddEnemy(newEnemy.GetComponent<Enemy>());
            }
        }
    }

    private bool CheckCollisions(Vector3 pos)
    {
        Collider[] hitColliders = Physics.OverlapSphere(pos, overlapSphereRadius);
        return hitColliders.Length > 0;
    }
}
