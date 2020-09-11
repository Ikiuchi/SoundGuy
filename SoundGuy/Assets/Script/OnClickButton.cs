using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class OnClickButton : MonoBehaviour
{
    public void PlaySound(AudioSource source)
    {
        source.Play();
    }
}
