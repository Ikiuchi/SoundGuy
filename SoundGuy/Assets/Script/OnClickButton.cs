using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OnClickButton : MonoBehaviour
{
    public AudioClip[]  audioClips;
    public Button[]     buttons;

    private AudioSource[] source;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        source = new AudioSource[audioClips.Length];

        for(int i = 0; i < source.Length; i++)
        {
            source[i].clip = audioClips[i];
            source[i].playOnAwake = false;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].onClick.AddListener(() => PlaySound(i));
        }
    }

    void PlaySound(int index)
    {
        source[index].Play();
    }
}
