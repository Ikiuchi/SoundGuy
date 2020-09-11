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

        for (int i = 0; i < source.Length; i++)
        {
            source[i] = SoundClick.AddComponent<AudioSource>();
            source[i].clip = audioClips[i];
            source[i].loop = true;
            source[i].playOnAwake = false;
            source[i].outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[2];
            //source[i].Play();
        }

        /*for (int i = 0; i < buttons.Length; i++)
        {
            Debug.Log("i " + i);
            buttons[i].onClick.AddListener(() => PlaySound(source[i]));
            source[i].Play();
        }*/
    }

    public void PlaySound(AudioSource source)
    {
        Debug.Log("hi");
        source.Play();
    }
}
