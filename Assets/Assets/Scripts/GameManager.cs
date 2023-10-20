using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider healthBar;
    public Text scoreText;
    public int maxHealth = 100;  // Valor máximo de salud
    private int health;  // Valor actual de salud
    private int score = 0;
    public AudioClip failSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        health = maxHealth / 2;  // Inicia la salud en la mitad del valor máximo
        healthBar.maxValue = maxHealth;  // Establece el valor máximo de la barra de salud
        healthBar.value = health;  // Establece el valor inicial de la barra de salud
    }

    public void NoteHit()
    {
        health += 5;  // Ajusta este valor para que el incremento de salud sea menos pronunciado
        score += 100;
        UpdateUI();
    }

    public void NoteMissed()
    {
        health -= 20;  // Ajusta este valor para que la disminución de salud sea más pronunciada
        score -= 50;
        AudioSource.PlayClipAtPoint(failSound, transform.position);
        UpdateUI();

        if (score < 0 || health <= 0)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        // Detener la música
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        // Guardar el puntaje (opcional)
        PlayerPrefs.SetInt("LastScore", score);

        // Cambiar a otra escena
        SceneManager.LoadScene("GameOverScene");  // Asumiendo que tienes una escena llamada "GameOverScene"
    }

    private void UpdateUI()
    {
        healthBar.value = health;
        scoreText.text = "Score: " + score;
    }

    // ... Resto del código ...
}
