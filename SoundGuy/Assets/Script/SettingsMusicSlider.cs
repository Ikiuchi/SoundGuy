using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMusicSlider : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Text musicVolumeText;
    public Text sfxVolumeText;


    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", musicVolume);

        musicVolumeText.text = ((musicVolume + 80) / 80 * 100).ToString();
    }

    public void SetSFXVolume(float SFXVolume)
    {
        audioMixer.SetFloat("sfxVolume", SFXVolume);
        sfxVolumeText.text = ((SFXVolume + 80) / 80 * 100).ToString();
    }
}
