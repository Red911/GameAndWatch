using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource playerAudio;

    public AudioClip hit;
    public AudioClip movement;
    public AudioClip getPatient;
    public AudioClip victory;
    
    
    void Start()
    {
        playerAudio = gameObject.AddComponent<AudioSource>();
        playerAudio.volume = 0.5f;
        
    }
}
