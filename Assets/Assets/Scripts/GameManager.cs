using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // Importa esta biblioteca para cambiar de escena

public class GameManager : MonoBehaviour
{
    public Slider healthBar;
    public Text scoreText;
    public int health = 100;
    private int score = 0;
    public AudioClip failSound;  // Agrega tu clip de audio de fallo aquí
    private AudioSource audioSource;  // Añade una fuente de audio

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void NoteHit()
    {
        health += 10;
        score += 100;
        UpdateUI();
    }

    public void NoteMissed()
    {
        health -= 10;
        score -= 50;  // Reduce el score cuando se pierde una nota
        AudioSource.PlayClipAtPoint(failSound, transform.position);  // Reproduce el sonido de fallo
        UpdateUI();

        // Comprueba si el juego debe terminar
        if (score < 0 || health <= 0)
        {
            EndGame();
        }
    }

    private void UpdateUI()
    {
        healthBar.value = health;
        scoreText.text = "Score: " + score;
    }

    private void EndGame()
    {
        audioSource.Stop();  // Detiene la música
        SceneManager.LoadScene("GameOverScene");  // Cambia a la escena de Game Over
    }

    void Update()
    {
        // Verifica si la canción ha terminado, asumiendo que RhythmEngine está en el mismo GameObject
        RhythmEngine rhythmEngine = GetComponent<RhythmEngine>();
        if (!rhythmEngine.audioSource.isPlaying)
        {
            // La canción ha terminado, carga la siguiente escena
            SceneManager.LoadScene("EndScene");
        }
    }
}