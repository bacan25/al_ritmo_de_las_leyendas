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
    public SongAsset songAsset;
    public GameObject[] notePrefabs;
    public Transform[] spawnPoints;
    private int nextNoteIndex = 0;
    private RhythmEngine rhythmEngine;
    void Start()
    {
        // Asume que hay un objeto llamado "AudioManager" que tiene el RhythmEngine y AudioSource
        GameObject audioManager = GameObject.Find("AudioManager");
        if (audioManager != null)
        {
            rhythmEngine = audioManager.GetComponent<RhythmEngine>();
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
        else
        {
            Debug.LogError("AudioManager object not found");
        }
    }
    void Update()
    {
        if (nextNoteIndex < song.notes.Length && rhythmEngine.audioSource.time >= song.notes[nextNoteIndex].time)
        {
            Debug.Log("Generating note at time: " + rhythmEngine.audioSource.time);
            int direction = (int)song.notes[nextNoteIndex].direction;
            Vector3 spawnPosition = spawnPoints[direction].position;
            Instantiate(notePrefabs[direction], spawnPosition, Quaternion.identity);
            nextNoteIndex++;
        }
    }


}


