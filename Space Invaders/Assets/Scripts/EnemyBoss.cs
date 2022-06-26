using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject enemyBlastPreFab;
    private Vector2 screenBoundRightTop;
    private Vector2 screenBoundLeftBottom;
    private Vector2 screenBounds;
    public ParticleSystem DestroyEffect;
    public int EnemyBossHealth = 20;
    private float respawnTime = 1.0f;

    public static object Instance { get; internal set; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Bullet":
                SpawnDestroyEffect();
                EnemyBossHealth--;
                if (EnemyBossHealth <= 0)
                {
                    Destroy(this.gameObject);
                    ScoreManager.Instance.addPoint(10);
                }
                break;
        }
    }

    private void SpawnEnemyBossBlast()
    {
        GameObject enemyBlast = Instantiate(enemyBlastPreFab) as GameObject;
        enemyBlast.transform.position = new Vector2(UnityEngine.Random.Range(-screenBounds.x + .2f, screenBounds.x - .2f), screenBounds.y - 4.5f);
    }

    IEnumerator SpawnBlast()
    {
        while (true)
        {
            yield return new WaitForSeconds(respawnTime);
            SpawnEnemyBossBlast();
        }
    }

    private void SpawnDestroyEffect()
    {
        Vector3 enemyPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(enemyPos.x, enemyPos.y, enemyPos.z + 0.1f);
        GameObject effect = Instantiate(DestroyEffect.gameObject, spawnPos, Quaternion.identity);
        Destroy(effect, DestroyEffect.main.startLifetime.constant);
    }

    private void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        screenBoundRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        screenBoundLeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        StartCoroutine(SpawnBlast());

    }
}
