using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JukeBox : MonoBehaviour
{
    [System.Serializable]
    public class Tracks
    {
        public string name;

        public bool Intro;
        public AudioClip IntroTrack;
        public int currentTrack;
        public AudioClip[] Track;
        [Tooltip("Scenes the track should be played in")]
        public string[] Scenes;
    }

    [SerializeField] List<Tracks> songs;
    Tracks currentTrack;
    AudioSource audioSource;

    private void OnEnable()
    {
        ChangeArea();
    }

    private void OnLevelWasLoaded()
    {
        ChangeArea();
    }

    private void ChangeArea()
    {
        if (FindObjectsOfType<JukeBox>().Length > 1)
            Destroy(gameObject);

        audioSource = GetComponent<AudioSource>();
        DontDestroyOnLoad(gameObject);

        foreach (Tracks track in songs)
            foreach (string scene in track.Scenes)
                if (scene == SceneManager.GetActiveScene().name)
                {
                    if (currentTrack == track) return;

                    currentTrack = track;
                    track.currentTrack = 0;
                    if (track.Intro)
                    {
                        audioSource.clip = track.IntroTrack;
                        track.currentTrack = -1;
                    }
                    else
                        audioSource.clip = track.Track[track.currentTrack];
                    audioSource.Play();
                    return;
                }
    }

    public void Update()
    {
        if (!audioSource.isPlaying)
            NextTrack();
    }

    private void NextTrack()
    {
        if (currentTrack.Track.Length < currentTrack.currentTrack)
            currentTrack.currentTrack++;
        else
            currentTrack.currentTrack = 0;

        audioSource.clip = currentTrack.Track[currentTrack.currentTrack];
        audioSource.Play();
    }

}
