using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    AudioSource boingSoundSource;

    AudioSource gameOverSoundSource;

    public GameObject boingSoundClass;

    public GameObject gameOverSoundClass;

    // Start is called before the first frame update
    void Start()
    {
        boingSoundSource = boingSoundClass.GetComponent<AudioSource>();
        gameOverSoundSource = gameOverSoundClass.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void boingSound()
    {
        boingSoundSource.Play();
    }

    public void gameOverSound()
    {
        gameOverSoundSource.Play();
    }
}
