using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    bool BulletIsGone;
    Rigidbody2D rb;
    private float Initial_Bullet_Speed = 15;
    
    public void StartFly()
    {
        BulletIsGone = true;
    }
    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Miss")
        {
            Destroy(this.gameObject);
        }
        else if (collision.transform.tag == "Enemy")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
            ScoreManager.Instance.addPoint(1);
        }
        else if (collision.transform.tag == "EnemyBoss")
        {
            Destroy(this.gameObject);
        }

    }

    private void Update()
    {
        if (BulletIsGone == true)
        {
            Vector2 currentPosition = new Vector2(transform.position.x, transform.position.y);
            rb.MovePosition(currentPosition + Vector2.up * Initial_Bullet_Speed * Time.deltaTime);
        }
    }
}
