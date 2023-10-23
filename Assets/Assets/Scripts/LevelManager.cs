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
        if (nextNoteIndex < songAsset.song.notes.Length && rhythmEngine.audioSource.time >= songAsset.song.notes[nextNoteIndex].time)
        {
            int direction = (int)songAsset.song.notes[nextNoteIndex].direction;
            Vector3 spawnPosition = spawnPoints[direction].position;
            Instantiate(notePrefabs[direction], spawnPosition, Quaternion.identity);
            nextNoteIndex++;
        }
    }
}
