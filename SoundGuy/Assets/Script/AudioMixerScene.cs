using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioMixerScene : MonoBehaviour
{
    public AudioMixer mixer;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
