using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HeartManager : MonoBehaviour
{
    public Image[] hearts; // Arrastra aquí las 3 imágenes de corazón desde inspector
    public int maxLives = 3;
    private int currentLives;

    private Coroutine blinkingCoroutine;

    void Start()
    {
        currentLives = maxLives;
        UpdateHeartsUI();
    }

    public void TakeDamage()
    {
        if (currentLives <= 0) return;

        // Parpadea el corazón actual para indicar daño
        if (blinkingCoroutine != null)
            StopCoroutine(blinkingCoroutine);

        blinkingCoroutine = StartCoroutine(BlinkHeart(hearts[currentLives - 1]));

        currentLives--;
        UpdateHeartsUI();

        if (currentLives <= 0)
        {
            Debug.Log("Jugador murió");
            // Aquí pones la lógica de muerte del jugador
        }
    }

    private IEnumerator BlinkHeart(Image heart)
    {
        float blinkDuration = 1f;
        float blinkSpeed = 0.2f;
        float elapsed = 0f;

        while (elapsed < blinkDuration)
        {
            heart.enabled = !heart.enabled;
            elapsed += blinkSpeed;
            yield return new WaitForSeconds(blinkSpeed);
        }
        heart.enabled = true; // Asegura que termine visible
    }

    void UpdateHeartsUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = i < currentLives;
        }
    }
}
