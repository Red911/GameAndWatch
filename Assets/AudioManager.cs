using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource playerAudio;
    public AudioSource enviromentAudio;

    [Header("Player Music")]
    public AudioClip hit;
    public AudioClip movement;
    public AudioClip getPatient;
    public AudioClip victory;

    [Header("Evironement Music")] 
    public AudioClip carDriving;
    
    
    void Start()
    {
        playerAudio = gameObject.AddComponent<AudioSource>();
        playerAudio.volume = 0.5f;
        
        enviromentAudio = gameObject.AddComponent<AudioSource>();
        enviromentAudio.volume = 0.6f;
        
    }
}
