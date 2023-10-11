using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmEngine : MonoBehaviour
{
    public AudioSource audioSource;
    public float bpm;
    private float beatInterval;
    private float nextBeatTime;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        beatInterval = 60f / bpm;
        nextBeatTime = audioSource.time + beatInterval;
        audioSource.Play();
    }

    void Update()
    {
        if (audioSource.time >= nextBeatTime)
        {
            nextBeatTime += beatInterval;
            // Lógica para manejar lo que sucede en cada beat.
        }
    }
}