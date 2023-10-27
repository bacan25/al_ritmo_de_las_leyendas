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
    public Text comboText; // Referencia a un objeto de texto UI para mostrar el combo
    private int comboCount = 0; // Contador de combo
    private int comboLevel = 1;
    public AudioClip failSound;
    private AudioSource audioSource;

    public Animator playerAnim;

    void Start()
    {
        playerAnim.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        health = 60;
        healthBar.maxValue = maxHealth;  // Establece el valor máximo de la barra de salud
        healthBar.value = health;  // Establece el valor inicial de la barra de salud
    }

    public void NoteHit()
    {
        if(health <= 100){
            health += 5;
        }
        
        comboCount++; 

        playerAnim.SetTrigger("press1");

        // Verificar si se ha alcanzado un nuevo nivel de combo
        if (comboCount % 10 == 0 && comboLevel < 10)
        {
            comboLevel++;
        }

        // Multiplicar la puntuación ganada por el nivel de combo
        score += 100 * comboLevel;

        UpdateUI();
    }
    public void NoteMissed()
    {
        playerAnim.SetTrigger("miss");
        health -= 20;
        score -= 50 * comboLevel; // También puedes considerar multiplicar la penalización por el nivel de combo
        comboCount = 0; // Reiniciar el contador de combo
        comboLevel = 1; // Reiniciar el nivel de combo
        AudioSource.PlayClipAtPoint(failSound, transform.position);
        UpdateUI();

        if (score < 0 || health <= 0)
        {
            EndGame();
        }
    }
    private void EndGame()
    {
        
        playerAnim.SetBool("lose", true);

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
    public void WinGame()
    {
        // Código para manejar ganar el juego, como cargar una nueva escena
        SceneManager.LoadScene("WinScene");
    }

    private void UpdateUI()
    {
        healthBar.value = health;
        scoreText.text = "Score: " + score;
        comboText.text = "Combo: " + comboCount; // Mostrar el combo
    }

    // ... Resto del código ...
}
