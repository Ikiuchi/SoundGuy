using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryButton : MonoBehaviour
{
	public Text score;

    public void Start()
    {
		if (SaveOptions.instance != null)
			score.text = SaveOptions.instance.score.ToString();
		else
			score.text = 0.ToString();
	}

    public void Replay()
	{
		SceneManager.LoadScene("TestElo");
	}

	public void Next()
	{
		SceneManager.LoadScene("MenuScene");
	}

}
