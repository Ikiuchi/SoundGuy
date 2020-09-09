﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;


    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void MusicSLider()
    {

    }

    public void ReturnToPause()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }
}