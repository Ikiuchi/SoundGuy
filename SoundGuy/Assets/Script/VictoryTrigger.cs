using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    public bool victory;
    private float timer;
    private float currentTimer = 0f;
    private bool needToUpdate = false;

    private AudioSource victorySound;
    public ParticleSystem[] victoryParticle = new ParticleSystem[3];
    GameObject child;

    public void Start()
    {
        timer = 6f;
        victorySound = GetComponent<AudioSource>();
        victorySound.Stop();
        child = transform.GetChild(0).gameObject;
        child.SetActive(false);
        victoryParticle[0] = child.transform.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        victoryParticle[1] = child.transform.GetChild(1).gameObject.GetComponent<ParticleSystem>();
        victoryParticle[2] = child.transform.GetChild(2).gameObject.GetComponent<ParticleSystem>();
    }

    public void Update()
    {
        if (!needToUpdate)
            return;

        currentTimer += Time.deltaTime;

        if(timer < currentTimer)
            SceneManager.LoadScene("VictoryScene");
    }

    public void OnTriggerEnter(Collider other)
    {
        Player player = other.GetComponent<Player>();
        if (other.GetComponent<Player>())
        {
            player.End = true;
            victorySound.Play();
            for(int i = 0; i < 3; i++)
            {
                victoryParticle[i].Play();
            }
            child.SetActive(true);
            needToUpdate = true;
        }
    }
}
