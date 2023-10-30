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
    public AudioSource audioSource;

    public CharacterAnimator playerAnim;
    public bool gameOver = false;


    void Start()
    {
        playerAnim.GetComponent<CharacterAnimator>();
        audioSource.GetComponent<AudioSource>();
        health = 70;
        healthBar.maxValue = maxHealth;  // Establece el valor máximo de la barra de salud
        healthBar.value = health;  // Establece el valor inicial de la barra de salud
    }

    void Update()
    {
        if (score < 0 || health <= 0)
        {
            gameOver = true;
            EndGame();
        }
    }

    public void NoteHit()
    {
        if(health <= 100){
            health += 5;
        }
        
        comboCount++; 
        playerAnim.AnimSelector();

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
        playerAnim.MissANote();
        if(gameOver == false){  
            health -= 1;
            if(score > 0){
                 score -= 50 * comboLevel;
            }
            comboCount = 0; // Reiniciar el contador de combo
            comboLevel = 1; // Reiniciar el nivel de combo
            AudioSource.PlayClipAtPoint(failSound, transform.position);
        
        }
        
        UpdateUI();

    }
    private void EndGame()
    {
        playerAnim.LoseGame();

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
        playerAnim.Won();
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
