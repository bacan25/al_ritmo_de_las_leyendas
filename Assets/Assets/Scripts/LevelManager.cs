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
    public SongAsset songAsset;
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;
    private int nextNoteIndex = 0;
    private RhythmEngine rhythmEngine;

    void Start()
    {
        rhythmEngine = GetComponent<RhythmEngine>();
        rhythmEngine.OnBeat += HandleBeat; // Suscribirse al evento OnBeat

        if (rhythmEngine.audioSource != null)
        {
            rhythmEngine.audioSource.clip = songAsset.audioClip;
            rhythmEngine.audioSource.Play();
        }
        else
        {
            Debug.LogError("RhythmEngine or AudioSource is not set");
        }
    }

    void HandleBeat() // Manejar cada beat
    {
        float resolution = 192; // Esto debe ser obtenido de tu archivo de la canción

        Debug.Log("HandleBeat called. nextNoteIndex: " + nextNoteIndex + ", songAsset.song.notes.Length: " + songAsset.song.notes.Length);

        if (nextNoteIndex < songAsset.song.notes.Length)
        {
            float noteTimeInBeats = songAsset.song.notes[nextNoteIndex].time;
            float noteTimeInSeconds = noteTimeInBeats / resolution * (60 / rhythmEngine.bpm);

            Debug.Log("AudioSource time: " + rhythmEngine.audioSource.time + ", noteTimeInSeconds: " + noteTimeInSeconds);

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
                    Debug.LogError("Invalid direction value: " + direction + ". Skipping note.");
                    nextNoteIndex++; // Incrementa nextNoteIndex para evitar quedarse atascado en una nota con un valor de dirección inválido
                }
            }
        }
    }


}