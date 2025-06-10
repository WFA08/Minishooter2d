using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 2f;
    public GameObject enemyBulletPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float fireTimer = 0f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        // Movimiento hacia el jugador
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            // Disparo
            fireTimer += Time.deltaTime;
            if (fireTimer >= fireRate)
            {
                Shoot(direction); // Dispara hacia el jugador
                fireTimer = 0f;
            }
        }
    }

    void Shoot(Vector2 direction)
    {
        GameObject bullet = Instantiate(enemyBulletPrefab, firePoint.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().linearVelocity = direction * 5f; // velocidad y dirección
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            
            GameManager.instance.AddScore(1);
        }
    }
}
