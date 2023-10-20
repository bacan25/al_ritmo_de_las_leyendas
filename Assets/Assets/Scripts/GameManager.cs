using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Slider healthBar;
    public Text scoreText; // Asegúrate de asignar un objeto Text de tu UI a esta variable en el Inspector.
    public int health = 100;
    private int score = 0; // Nueva variable para manejar la puntuación.

    public void NoteHit()
    {
        health += 10;
        score += 100; // Aumenta la puntuación cuando se golpea una nota.
        UpdateUI();
    }

    public void NoteMissed()
    {
        health -= 10;
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.value = health;
        scoreText.text = "Score: " + score; // Actualiza el texto de la puntuación.
    }
}