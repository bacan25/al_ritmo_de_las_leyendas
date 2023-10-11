using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInput : MonoBehaviour
{
    public GameManager gameManager;
    public SpriteRenderer[] inputCircles;  // Cambiado de Image[] a SpriteRenderer[]
    public Collider2D[] hitBars;

    void Update()
    {
        CheckInput(KeyCode.LeftArrow, 0);  // Asume que el índice 0 corresponde a la izquierda
        CheckInput(KeyCode.DownArrow, 1);  // Asume que el índice 1 corresponde a abajo
        CheckInput(KeyCode.UpArrow, 2);    // Asume que el índice 2 corresponde a arriba
        CheckInput(KeyCode.RightArrow, 3); // Asume que el índice 3 corresponde a la derecha
    }

    void CheckInput(KeyCode key, int index)
    {
        if (Input.GetKeyDown(key))
        {
            inputCircles[index].color = Color.white;  // Cambia el color a blanco (brillante) cuando la tecla es presionada
            Collider2D[] notes = Physics2D.OverlapBoxAll(hitBars[index].bounds.center, hitBars[index].bounds.size, 0f);
            bool noteHit = false;  // Variable para rastrear si se ha golpeado una nota

            // Itera sobre todos los colliders devueltos por OverlapBoxAll
            foreach (Collider2D note in notes)
            {
                if (note.CompareTag("Note"))
                {
                    // Si el collider es una nota, destrúyelo y actualiza la variable noteHit
                    Destroy(note.gameObject);
                    noteHit = true;
                    break;  // Rompe el bucle una vez que una nota ha sido golpeada
                }
            }

            // Actualiza la vida del jugador en base a si se golpeó una nota o no
            if (noteHit)
            {
                gameManager.NoteHit();
            }
            else
            {
                gameManager.NoteMissed();
            }
        }
        else if (Input.GetKeyUp(key))
        {
            inputCircles[index].color = new Color(0.5f, 0.5f, 0.5f);  // Restaura el color a gris (menos brillante) cuando la tecla es liberada
        }
    }
}
