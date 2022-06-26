using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    public GameObject enemyPreFab, enemyBossPreFab;
    private float respawnTime = 1.0f;
    private Vector2 screenBounds;
    private int enemyCount;

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(SpawnWave());
    }
    private void SpawnEnemy()
    {
        GameObject enemy = Instantiate(enemyPreFab) as GameObject;
        enemy.transform.position = new Vector2(Random.Range(-screenBounds.x + .3f, screenBounds.x - .3f), screenBounds.y - 1f);
        enemyCount++;
    }
    private void SpawnEnemyBoss()
    {
        GameObject enemyBoss = Instantiate(enemyBossPreFab) as GameObject;
        enemyBoss.transform.position = new Vector2(0f, 2.63f);
        enemyCount += 10;
    }

    
    IEnumerator SpawnWave()
    {
        while (Spaceship.Instance.health > 0)
        {
            if (enemyCount < 10)
            {
                yield return new WaitForSeconds(respawnTime * 1.5f);
                SpawnEnemy();
            }
            else if (enemyCount is >= 10 and < 40)
            {
                yield return new WaitForSeconds(respawnTime / 3);
                SpawnEnemy();
            }
            else if (enemyCount is >= 40 and < 50)
            {
                yield return new WaitForSeconds(respawnTime * 3);
                SpawnEnemyBoss();

            }
            else if (enemyCount >= 50)
            {
                break;
            }
        }
        
    }
}
