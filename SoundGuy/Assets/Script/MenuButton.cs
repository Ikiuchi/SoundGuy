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

	public void Start()
	{
		panelLevel.SetActive(false);
	}

	public void Play()
	{
		panelLevel.SetActive(!panelLevel.activeSelf);
		panelButton.SetActive(!panelButton.activeSelf);
	}

	public void Options()
	{
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
