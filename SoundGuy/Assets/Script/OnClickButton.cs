using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class OnClickButton : MonoBehaviour
{
    public AudioClip[]  audioClips;
    public Button[]     buttons;
    public AudioMixer   mixer;

    private AudioSource[] source;
    public GameObject SoundClick;
    // Start is called before the first frame update
    void Start()
    {
        source = new AudioSource[audioClips.Length];
        if (!mixer)
            return;

        for (int i = 0; i < source.Length - 1; i++)
        {
            source[i] = SoundClick.AddComponent<AudioSource>();
            source[i].clip = audioClips[i];
            //source[i].playOnAwake = false;
            source[i].outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[2];
        }

        for (int i = 0; i < buttons.Length - 1; i++)
        {
            //Debug.Log(i);
            //buttons[i].onClick.AddListener(() => PlaySound(i));
        }
    }

    void PlaySound(int index)
    {
        source[index].Play();
    }
}
