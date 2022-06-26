using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBlast : MonoBehaviour
{
    private float speed = 2;
    private Rigidbody2D rb;
    private Vector2 screenBoundRightTop;
    private Vector2 screenBoundLeftBottom;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            Destroy(this.gameObject);
        }
        else if (collision.transform.tag == "Bullet")
        {
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
    private void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -speed);
        screenBoundLeftBottom = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z));
        screenBoundRightTop = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    private void Update()
    {
        if(this.transform.position.y < screenBoundLeftBottom.y - 2)
        {
            Destroy(this.gameObject);
        }
    }
}
