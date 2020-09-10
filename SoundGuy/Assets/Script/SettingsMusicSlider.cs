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

    private void Awake()
    {
        float volume;
        if (audioMixer.GetFloat("musicVolume", out volume))
        {
            Debug.Log((volume + 80) / 80 * 100);
            musicVolumeText.text = ((int)(volume + 80) / 80 * 100).ToString();

            musicVolumeSlider.SetValueWithoutNotify(volume);
        }
        if (audioMixer.GetFloat("sfxVolume", out volume))
        {
            sfxVolumeText.text = ((volume + 80) / 80 * 100).ToString();
            sfxVolumeSlider.SetValueWithoutNotify(volume);
        }
    }

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
