using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Definición del enum para las direcciones
public enum Direction
{
    Left,
    Down,
    Up,
    Right
}

[System.Serializable]
public class Note
{
    public float time;
    public Direction direction;  // Cambio de int a Direction
    /*...*/
}

[System.Serializable]
public class Song
{
    public AudioClip audioClip;
    public Note[] notes;
    /*...*/
}


public class LevelManager : MonoBehaviour
{
    public SongAsset songAsset; // Asegúrate de que esté declarada así
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;
    private int nextNoteIndex = 0;
    private RhythmEngine rhythmEngine;

    void Start()
    {
        rhythmEngine = GetComponent<RhythmEngine>();
        if (rhythmEngine != null && rhythmEngine.audioSource != null)
        {
            rhythmEngine.audioSource.clip = songAsset.audioClip;
            rhythmEngine.audioSource.Play();
        }
        else
        {
            Debug.LogError("RhythmEngine or AudioSource is not set");
        }
    }
    void Update()
    {
        Debug.Log("Rhythm Engine: " + rhythmEngine);
        Debug.Log("Audio Source: " + rhythmEngine.audioSource);
        Debug.Log("Song Asset: " + songAsset);

        if (nextNoteIndex < songAsset.notes.Length && rhythmEngine.audioSource.time >= songAsset.notes[nextNoteIndex].time)
        {
            Debug.Log("Instantiating Note");
            int direction = (int)songAsset.notes[nextNoteIndex].direction;
            Vector3 spawnPosition = spawnPoints[direction].position;
            Instantiate(notePrefabs[direction], spawnPosition, Quaternion.identity);
            nextNoteIndex++;
        }
    }

}


