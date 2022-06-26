using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    #region Singleton
    private static BulletManager _instance;
    public static BulletManager Instance => _instance;
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
    [SerializeField]
    private Bullet Bullet_PreFab;
    private Bullet Initial_Bullet;
    private Rigidbody2D Initial_Bullet_RB;
    

    public List<Bullet> Bullets { get; set; }
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InitBullets();
            Initial_Bullet.StartFly();
            GameManager.Instance.IsGameStarted = true;

        }
        
    }

    private void InitBullets()
    {
        Vector3 spaceship_Postion = Spaceship.Instance.gameObject.transform.position;
        Vector3 starting_Position = new Vector3(spaceship_Postion.x, spaceship_Postion.y + 1.0f, 0);
        Initial_Bullet = Instantiate(Bullet_PreFab, starting_Position, Quaternion.identity);
        Initial_Bullet_RB = Initial_Bullet.GetComponent<Rigidbody2D>();

        this.Bullets = new List<Bullet>
        { Initial_Bullet };
    }
}
