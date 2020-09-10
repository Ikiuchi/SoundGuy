using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOptions : MonoBehaviour
{
	public static SaveOptions instance;

	private bool invertXAxis = false;
    private bool invertYAxis = false;

	Player player;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
			Destroy(this);
	}

	public void UpdateXAxis(bool b)
	{
		if (player == null)
			player = FindObjectOfType<Player>();

		invertXAxis = b;

		player.UpdateXAxis(invertXAxis);
	}

	public void UpdateYAxis(bool b)
	{
		if (player == null)
			player = FindObjectOfType<Player>();

		invertYAxis = b;

		player.UpdateXAxis(invertYAxis);
	}
}
