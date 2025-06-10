using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        // Mover la bala hacia arriba (o en la dirección que quieras)
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    // Destruir la bala cuando salga de la cámara para no acumular objetos
    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
