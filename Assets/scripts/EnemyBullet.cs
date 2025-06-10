using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Intenta obtener el componente PlayerHealth y hacerle daño
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage();
            }

            Destroy(gameObject); // Destruye la bala después de hacer daño
        }
        else if (!collision.CompareTag("Enemy"))
        {
            Destroy(gameObject); // Se destruye si toca otra cosa que no sea enemigo
        }
    }
}
