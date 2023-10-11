using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Song", menuName = "Song")]
public class SongAsset : ScriptableObject
{
    public Song song;
    public AudioClip audioClip;
    public Note[] notes;
    // ...otros miembros y métodos...
}
