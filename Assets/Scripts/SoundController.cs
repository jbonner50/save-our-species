using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{

    public static AudioSource[] audioSources;

    // Start is called before the first frame update
    void Start()
    {
        audioSources = GetComponents<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public static void playSunSound()
    {
        audioSources[1].Play();
    }

    public static void playImpactSound()
    {
        audioSources[2].Play();

    }

    public static void playCapsuleSound()
    {
        audioSources[3].Play();

    }
}
