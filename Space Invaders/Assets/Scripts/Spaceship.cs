using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spaceship : MonoBehaviour
{
    #region Singleton
    private static Spaceship _instance;
    public static Spaceship Instance => _instance;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion
    private float _rightBorder;
    private float _leftBorder;
    private float _topBorder;
    private float _bottomBorder;
    private float SpaceCraftWidth = 1.0f;
    public float health = 3;
    Camera mainCamera;
    [SerializeField] GameOverManager gameOverManager;
    public ParticleSystem DestroyEffectPlayer;
    
    void Start()
    {
        Cursor.visible = false;
        mainCamera = FindObjectOfType<Camera>();
        _leftBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).x + SpaceCraftWidth / 2;
        _rightBorder = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0)).x - SpaceCraftWidth / 2;
        _topBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height/2)).y - SpaceCraftWidth / 2;
        _bottomBorder = Camera.main.ScreenToWorldPoint(new Vector3(0, 0)).y + SpaceCraftWidth / 2;
    }
    // Delete OnCollision from player if it is not working
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            SpawnDestroyEffect();
            health--;
            if (health == 0)
            {
                Destroy(this.gameObject);
                gameOverManager.SetGameOver();
            }
        }
        else if (collision.transform.tag == "EnemyBoss")
        {
            Destroy(this.gameObject);
            SpawnDestroyEffect();
            gameOverManager.SetGameOver();
        }
        else if (collision.transform.tag == "EnemyBlast")
        {
            Destroy(this.gameObject);
            SpawnDestroyEffect();
            gameOverManager.SetGameOver();
        }
    }
    private void SpawnDestroyEffect()
    {
        Vector3 playerPos = gameObject.transform.position;
        Vector3 spawnPos = new Vector3(playerPos.x, playerPos.y, playerPos.z + 0.1f);
        GameObject effect = Instantiate(DestroyEffectPlayer.gameObject, spawnPos, Quaternion.identity);
        Destroy(effect, DestroyEffectPlayer.main.startLifetime.constant);
    }
    void Update()
    {
        SpaceshipMovement();
    }

    private void SpaceshipMovement()
    {
        Vector3 mousePositionWorld = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        mousePositionWorld.x = Mathf.Clamp(mousePositionWorld.x, _leftBorder, _rightBorder);
        mousePositionWorld.y = Mathf.Clamp(mousePositionWorld.y, _bottomBorder, _topBorder);
        this.transform.position = new Vector3(mousePositionWorld.x, mousePositionWorld.y + 0.1f, 0);
    }
}


