using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
	public GameObject panelButton;
	public GameObject panelLevel;
	public GameObject panelSettings;

	public void Start()
	{
		panelLevel.SetActive(false);
		panelSettings.SetActive(false);
	}

	public void Play()
	{
		panelLevel.SetActive(!panelLevel.activeSelf);
		panelButton.SetActive(!panelButton.activeSelf);
		panelSettings.SetActive(false);
	}

	public void Options()
	{
		panelLevel.SetActive(false);
		panelButton.SetActive(false);
		panelSettings.SetActive(true);
	}

	public void Return()
	{
		panelLevel.SetActive(false);
		panelButton.SetActive(true);
		panelSettings.SetActive(false);
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
		SceneManager.LoadScene("GameScene");
	}
}
