using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement;

    void Update()
    {
        // Movimiento básico
        movement.x = Input.GetAxis("Horizontal");
        movement.y = Input.GetAxis("Vertical");
        transform.Translate(movement * speed * Time.deltaTime);
    }

    // ESTE ES EL BLOQUE QUE AGREGAMOS:
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<PlayerHealth>().TakeDamage(); // Le quita media vida al jugador
            
            Destroy(collision.gameObject);             // Elimina al enemigo que lo golpeó
        }
    }
}
