using System.Collections;
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

        SceneManager.LoadScene("GameScene");
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

    public void ReturnToPause()
    {
        pausePanel.SetActive(true);
        settingsPanel.SetActive(false);
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
