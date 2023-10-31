using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Direction
{
    Left,
    Down,
    Up,
    Right
}

public class LevelManager : MonoBehaviour
{
    private float lastNoteTime;
    public SongAsset songAsset;
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;
    private int nextNoteIndex = 0;
    public GameManager gameManager; // Referencia al GameManager

    private RhythmEngine rhythmEngine;

    //Animaciones de las leyendas
    [SerializeField]
    private float songDuration;
    [SerializeField]
    private float songPart;
    private float enemyDamaged;
    private float cronometer;
    private float checkDurationSong;
    public EnemyAnimator enemyAnim;
    private float timeSinceLastNote = 0f; // Contador desde la última nota



    void Start()
    {
        rhythmEngine = GetComponent<RhythmEngine>();
        enemyAnim.GetComponent<EnemyAnimator>();

        rhythmEngine.OnBeat += HandleBeat; // Suscribirse al evento OnBeat

        if (rhythmEngine.audioSource != null)
        {
            rhythmEngine.audioSource.clip = songAsset.audioClip;
            rhythmEngine.audioSource.Play();
        }
        else
        {
            //Debug.LogError("RhythmEngine or AudioSource is not set");
        }
    }
    void Update()
    {
        int notesInScene = GameObject.FindGameObjectsWithTag("Note").Length;

        // Si no hay notas en la escena, incrementar el contador
        if (notesInScene == 0)
        {
            timeSinceLastNote += Time.deltaTime;

            if (timeSinceLastNote >= 3f)
            {
                gameManager.WinGame();
                Debug.Log("Win Game");
                timeSinceLastNote = 0f; // Restablecer el contador
            }
        }
        else // Si hay notas en la escena, restablecer el contador
        {
            timeSinceLastNote = 0f;
        }
    }


    void HandleBeat() // Manejar cada beat
    {
        float resolution = 192; // Esto debe ser obtenido de tu archivo de la canción

        //Debug.Log("HandleBeat called. nextNoteIndex: " + nextNoteIndex + ", songAsset.song.notes.Length: " + songAsset.song.notes.Length);

        if (nextNoteIndex < songAsset.song.notes.Length)
        {
            float noteTimeInBeats = songAsset.song.notes[nextNoteIndex].time;
            float noteTimeInSeconds = noteTimeInBeats / resolution * (60 / rhythmEngine.bpm);

            //Debug.Log("AudioSource time: " + rhythmEngine.audioSource.time + ", noteTimeInSeconds: " + noteTimeInSeconds);

            if (rhythmEngine.audioSource.time >= noteTimeInSeconds)
            {
                int direction = (int)songAsset.song.notes[nextNoteIndex].direction;

                // Ajustar valores de dirección fuera de rango
                if (direction > 3)
                {
                    direction = 3; // Cambiar valores de dirección mayores que 3 a 3
                }

                // Ahora puedes usar 'direction' para instanciar la nota como lo hacías antes
                if (direction >= 0 && direction < spawnPoints.Length && direction < notePrefabs.Length)
                {
                    Vector3 spawnPosition = spawnPoints[direction].position;
                    Instantiate(notePrefabs[direction], spawnPosition, Quaternion.identity);
                    nextNoteIndex++;
                }
                else
                {
                    //Debug.LogError("Invalid direction value: " + direction + ". Skipping note.");
                    nextNoteIndex++; // Incrementa nextNoteIndex para evitar quedarse atascado en una nota con un valor de dirección inválido
                }
            }
        }
    }


}