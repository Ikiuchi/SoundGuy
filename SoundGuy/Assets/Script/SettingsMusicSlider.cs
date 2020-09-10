using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMusicSlider : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Text musicVolumeText;
    public Slider musicVolumeSlider;
    public Text sfxVolumeText;
    public Slider sfxVolumeSlider;

    private float valuePurcentage = 40;

    private void Awake()
    {
        float volume;
        if (audioMixer.GetFloat("musicVolume", out volume))
        {
            musicVolumeText.text = ((int)((volume + valuePurcentage) / valuePurcentage * 100)).ToString();

            musicVolumeSlider.SetValueWithoutNotify((int)volume);
        }
        if (audioMixer.GetFloat("sfxVolume", out volume))
        {
            sfxVolumeText.text = ((int)((volume + valuePurcentage) / valuePurcentage * 100)).ToString();
            sfxVolumeSlider.SetValueWithoutNotify((int)volume);
        }
    }

    public void SetMusicVolume(float musicVolume)
    {
        audioMixer.SetFloat("musicVolume", (int)musicVolume);

        musicVolumeText.text = ((int)((musicVolume + valuePurcentage) / valuePurcentage * 100)).ToString();
    }

    public void SetSFXVolume(float SFXVolume)
    {
        audioMixer.SetFloat("sfxVolume", (int)SFXVolume);
        sfxVolumeText.text = ((int)((SFXVolume + valuePurcentage) / valuePurcentage * 100)).ToString();
    }
}
