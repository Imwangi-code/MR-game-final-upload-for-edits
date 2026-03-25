using UnityEngine;

//calculates the position of the song in beats and seconds, allowing other scripts to sync with the music
public class Conductor : MonoBehaviour
{
    public float songBpm; 
    public float firstBeatOffset; // Seconds before the first beat starts
    public AudioSource musicSource;

    public float songPosition; // Current time in seconds
    public float songPositionInBeats; // Current time in beats
    public float dspSongTime; // Technical time for high precision

    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        dspSongTime = (float)AudioSettings.dspTime;
        musicSource.Play();
    }

    void Update()
    {
        // Calculate the exact position of the song using high-precision audio clock
        songPosition = (float)(AudioSettings.dspTime - dspSongTime - firstBeatOffset);
        
        // Convert seconds to beats: (seconds * (beats / 60 seconds))
        songPositionInBeats = songPosition / (60f / songBpm);
    }
}