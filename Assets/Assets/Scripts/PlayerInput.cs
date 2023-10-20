using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject[] spawnPoints;

    void Update()
    {
        CheckInput(KeyCode.LeftArrow, 0);
        CheckInput(KeyCode.DownArrow, 1);
        CheckInput(KeyCode.UpArrow, 2);
        CheckInput(KeyCode.RightArrow, 3);
    }

    void CheckInput(KeyCode key, int index)
    {
        GameObject spawnPoint = spawnPoints[index];
        Transform detecTransform = spawnPoint.transform.GetChild(0);
        SpriteRenderer detecSpriteRenderer = detecTransform.GetComponent<SpriteRenderer>();
        Collider2D detecCollider = detecTransform.GetComponent<Collider2D>();

        if (Input.GetKeyDown(key))
        {
            Collider2D[] notes = Physics2D.OverlapBoxAll(detecCollider.bounds.center, detecCollider.bounds.size, 0f);
            bool noteHit = false;

            foreach (Collider2D note in notes)
            {
                if (note.CompareTag("Note"))
                {
                    Destroy(note.gameObject);
                    noteHit = true;
                    break;
                }
            }

            if (noteHit)
            {
                detecSpriteRenderer.color = Color.green;
                gameManager.NoteHit();
            }
            else
            {
                detecSpriteRenderer.color = Color.red;
                gameManager.NoteMissed();
            }
        }
        else if (Input.GetKeyUp(key))
        {
            detecSpriteRenderer.color = Color.white;
        }
    }
}
