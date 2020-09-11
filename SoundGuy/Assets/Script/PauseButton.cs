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

    public GameObject buttonSelectedSettings;
    public GameObject buttonSelectedReturnToPause;
    public EventSystem eventSystem;

    public Toggle inversX;
    public Toggle inversY;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanel.SetActive(false);
    }

    public void Resume()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseScene");
    }

    public void Settings()
    {
        inversX.isOn = SaveOptions.instance.invertXAxis;
        inversY.isOn = SaveOptions.instance.invertYAxis;

        settingsPanel.SetActive(true);
        pausePanel.SetActive(false);
        eventSystem.SetSelectedGameObject(buttonSelectedSettings.gameObject);
    }

    public void QuitLevel()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void ReturnToPause()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false); 
        eventSystem.SetSelectedGameObject(buttonSelectedReturnToPause.gameObject);
    }

    public void AxisX(bool b)
    {
        SaveOptions.instance.UpdateXAxis(b);
    }
    public void AxisY(bool b)
    {
        SaveOptions.instance.UpdateYAxis(b);
    }
}
