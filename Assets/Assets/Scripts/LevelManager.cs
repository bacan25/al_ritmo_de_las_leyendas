using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Note { public float time; /*...*/ }

[System.Serializable]
public class Song { public AudioClip audioClip; public Note[] notes; /*...*/ }

public class LevelManager : MonoBehaviour
{
    public Song song;
    public GameObject notePrefab;
    private int nextNoteIndex = 0;

    void Start()
    {
        RhythmEngine rhythmEngine = GetComponent<RhythmEngine>();
        rhythmEngine.audioSource.clip = song.audioClip;
        rhythmEngine.audioSource.Play();
    }

    void Update()
    {
        RhythmEngine rhythmEngine = GetComponent<RhythmEngine>();
        if (nextNoteIndex < song.notes.Length && rhythmEngine.audioSource.time >= song.notes[nextNoteIndex].time)
        {
            Vector3 spawnPosition = new Vector3(/*x, y, z*/);  // Define la posición de spawn aquí
            Instantiate(notePrefab, spawnPosition, Quaternion.identity);
            nextNoteIndex++;
        }
    }

}