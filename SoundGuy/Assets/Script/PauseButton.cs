using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;

    public GameObject buttonSelected;
    public EventSystem eventSystem;
    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void Resume()
    {
        pausePanel.SetActive(false);

        SceneManager.LoadScene("TestElo");
    }

    public void Settings()
    {
        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
        eventSystem.SetSelectedGameObject(buttonSelected.gameObject);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ReturnToPause()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void AxisX(bool b)
	{
        if(SaveOptions.instance != null)
            SaveOptions.instance.UpdateXAxis(b);
}
    public void AxisY(bool b)
    {
        if (SaveOptions.instance != null)
            SaveOptions.instance.UpdateYAxis(b);
    }
}
