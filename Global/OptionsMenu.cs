using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [SerializeField]
    Slider masterVolumeSlider;

    [SerializeField]
    Slider sfxVolumeSlider;

    [SerializeField]
    Slider musicVolumeSlider;

    [SerializeField]
    AudioMixer masterMixer;

    [SerializeField]
    AudioMixer sfxMixer;

    [SerializeField]
    AudioMixer musicMixer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        masterMixer.SetFloat("MasterVol", Mathf.Log10(masterVolumeSlider.value) * 20);
        sfxMixer.SetFloat("SFXVol", Mathf.Log10(sfxVolumeSlider.value) * 20);
        musicMixer.SetFloat("MusicVol", Mathf.Log10(musicVolumeSlider.value) * 20);
    }
}
