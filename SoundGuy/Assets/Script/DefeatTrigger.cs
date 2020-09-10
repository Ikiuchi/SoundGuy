using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DefeatTrigger : MonoBehaviour
{
    public bool victory;
    private float timer;
    private float currentTimer;
    private bool needToUpdate = false;

    private AudioSource defeatSound;

    public void Start()
    {
        timer = 7f;
        defeatSound = GetComponent<AudioSource>();
        defeatSound.Stop();
    }

    public void Update()
    {
        if (!needToUpdate)
            return;

        currentTimer += Time.deltaTime;

        if (timer < currentTimer)
            SceneManager.LoadScene("DefeatScene");
    }

    private void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (player != null)
        {
            defeatSound.Play();
            needToUpdate = true;
        }
    }
}
