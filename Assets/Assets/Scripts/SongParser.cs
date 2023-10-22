#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class SongParser : MonoBehaviour
{
    public string filePath;  // Ruta del archivo para leer
    public SongAsset songAsset;  // Referencia a tu ScriptableObject

    void Start()
    {
        ParseSong(filePath);
    }

    void ParseSong(string path)
    {
        string fileContents;
        using (StreamReader reader = new StreamReader(path))
        {
            fileContents = reader.ReadToEnd();
        }

        string[] sections = fileContents.Split(new string[] { "[Song]", "[SyncTrack]", "[ExpertSingle]" }, System.StringSplitOptions.None);

        // Declarar songName y newSongAsset al inicio del método
        string songName = string.Empty;
        SongAsset newSongAsset = ScriptableObject.CreateInstance<SongAsset>();

        // Parse Song Section
        string[] songLines = sections[0].Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        foreach (string line in songLines)
        {
            // Eliminar espacios en blanco al inicio y al final de la línea
            string trimmedLine = line.Trim();

            // Ahora verificar si la línea comienza con "Name = "
            if (trimmedLine.StartsWith("Name = "))
            {
                // Extraer el nombre de la canción
                songName = trimmedLine.Substring(7).Trim('"');  // Asumiendo que el nombre está entre comillas

                // Configurar las propiedades del nuevo SongAsset
                newSongAsset.song = new Song { Name = songName };

                // Guardar el nuevo SongAsset
                SaveSongAsset(newSongAsset);
                break;  // Salir del bucle una vez que hayas encontrado y procesado el nombre de la canción
            }
        }

        // Aquí asumo que tienes una forma de obtener el AudioClip. Si no la tienes, 
        // tendrás que determinar cómo obtener o asignar el AudioClip.
        AudioClip audioClip = null;  // Reemplaza null con tu AudioClip

        // Parse ExpertSingle Section
        string[] expertSingleLines = sections[2].Split(new string[] { "\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        List<Note> notesList = new List<Note>();
        foreach (string line in expertSingleLines)
        {
            string[] parts = line.Split(' ');
            if (parts.Length >= 3 && parts[1] == "N")
            {
                int time = int.Parse(parts[0]);
                int noteType = int.Parse(parts[2]);
                Direction direction = (Direction)(noteType - 1);
                Note newNote = new Note
                {
                    time = time / 1000f,  // Convierte milisegundos a segundos
                    direction = direction
                };
                notesList.Add(newNote);  // Aquí llenas la lista de notas
            }
        }

        // Configurar las propiedades del newSongAsset ya existente
        newSongAsset.song.audioClip = audioClip;
        newSongAsset.song.notes = notesList.ToArray();

        // Guardar el newSongAsset ya existente
        SaveSongAsset(newSongAsset);
    }

#if UNITY_EDITOR
    void SaveSongAsset(SongAsset asset)
    {
        string path = "Assets/Resources/Songs/" + asset.song.Name + ".asset";
        AssetDatabase.CreateAsset(asset, path);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
#endif
}
