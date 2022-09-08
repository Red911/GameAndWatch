using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource playerAudio;
    public AudioSource environementAudio;

    public AudioClip hit;
    public AudioClip movement;
    public AudioClip getPatient;
    public AudioClip victory;
    
    
    void Start()
    {
        playerAudio = gameObject.AddComponent<AudioSource>();
        environementAudio = gameObject.AddComponent<AudioSource>();
        playerAudio.volume = 0.5f;
        environementAudio.volume = 0.5f;
    }
}
