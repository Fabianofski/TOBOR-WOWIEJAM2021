using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JukeBox : MonoBehaviour
{
    [System.Serializable]
    public class Tracks
    {
        public AudioClip Track;
        [Tooltip("Scenes the track should be played in")]
        public Scene[] Scenes;
    }

    [SerializeField] List<Tracks> songs;
    AudioSource audioSource;

    private void Awake()
    {
        if (FindObjectsOfType<JukeBox>().Length > 1)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        foreach (Tracks track in songs)
            foreach (Scene scene in track.Scenes)
                if (scene.name == SceneManager.GetActiveScene().name)
                {
                    audioSource.clip = track.Track;
                    return;
                }
    }

}
