using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour
{
	public GameObject panelButton;
	public GameObject panelLevel;
	public GameObject panelSettings;

	public GameObject buttonSelectedSettings;
	public GameObject buttonSelectedReturnToMenu;
	public GameObject buttonSelectedLevel;
	public EventSystem eventSystem;

	public Toggle inversX;
	public Toggle inversY;

	public GameObject firstSelected;


	public void Start()
	{
		panelLevel.SetActive(false);
		panelSettings.SetActive(false);
		eventSystem.SetSelectedGameObject(firstSelected.gameObject);
	}

	public void Play()
	{
		panelLevel.SetActive(!panelLevel.activeSelf);
		panelButton.SetActive(!panelButton.activeSelf);
		panelSettings.SetActive(false);
		eventSystem.SetSelectedGameObject(buttonSelectedLevel.gameObject);
	}

	public void Options()
	{
		inversX.isOn = SaveOptions.instance.invertXAxis;
		inversY.isOn = SaveOptions.instance.invertYAxis;

		panelLevel.SetActive(false);
		panelButton.SetActive(false);
		panelSettings.SetActive(true);
		eventSystem.SetSelectedGameObject(buttonSelectedSettings.gameObject);
	}


	public void AxisX(bool b)
	{
		SaveOptions.instance.UpdateXAxis(b);
	}
	public void AxisY(bool b)
	{
		SaveOptions.instance.UpdateYAxis(b);
	}

	public void Return()
	{
		panelLevel.SetActive(false);
		panelButton.SetActive(true);
		panelSettings.SetActive(false);
		eventSystem.SetSelectedGameObject(buttonSelectedReturnToMenu.gameObject);
	}

	public void Quit()
	{
	#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
	#else
		Application.Quit();
	#endif
	}

	public void Level1()
	{
		SceneManager.LoadScene("TestElo");
	}
}
