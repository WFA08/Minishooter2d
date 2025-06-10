using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public GameObject gameOverPanel;
    public Image[] hearts; // Asignar 3 corazones en el Inspector
    public float blinkDuration = 0.5f;

    private int[] heartHits; // Cada corazón puede tener 0, 1 o 2 golpes
    private int currentHeartIndex = 0;
    private bool isBlinking = false;

    void Start()
    {
        heartHits = new int[hearts.Length];
        for (int i = 0; i < heartHits.Length; i++)
            heartHits[i] = 0;

        UpdateHeartsVisual();
    }

    public void TakeDamage()
    {
        if (currentHeartIndex >= hearts.Length || isBlinking) return;

        heartHits[currentHeartIndex]++;

        StartCoroutine(BlinkHeart(currentHeartIndex));

        if (heartHits[currentHeartIndex] >= 2)
        {
            currentHeartIndex++;
        }

        UpdateHeartsVisual();

        if (currentHeartIndex >= hearts.Length)
        {
            Die();
        }
    }

    IEnumerator BlinkHeart(int index)
    {
        isBlinking = true;
        Image heart = hearts[index];

        float elapsed = 0f;
        while (elapsed < blinkDuration)
        {
            heart.enabled = !heart.enabled;
            yield return new WaitForSeconds(0.1f);
            elapsed += 0.1f;
        }

        heart.enabled = heartHits[index] < 2; // Si el corazón está roto, que desaparezca
        isBlinking = false;
    }

    void UpdateHeartsVisual()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].enabled = heartHits[i] < 2;
        }
    }

    void Die()
    {
        Debug.Log("GAME OVER");

        if (GameManager.instance != null)
        {
            GameManager.instance.ResetScore(); // Resetea el puntaje al morir
        }

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Destroy(gameObject);
    }
}
