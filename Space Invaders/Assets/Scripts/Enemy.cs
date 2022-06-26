using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    private float speed = 3.0f;
    private Rigidbody2D rb;
    private Vector2 screenBoundRightTop;
    private Vector2 screenBoundLeftBottom;
    public ParticleSystem DestroyEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.transform.tag)
        {
            case "Bullet":
            case "Player":
                SpawnDestroyEffect();
                break;
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
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
        screenBoundRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        screenBoundLeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));

    }
    private void Update()
    {
        if (this.transform.position.y < screenBoundLeftBottom.y - 2)
        {
            Destroy(this.gameObject);
        }
    }
}
