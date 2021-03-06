﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveOptions : MonoBehaviour
{
	public static SaveOptions instance;

	public bool invertXAxis = false;
    public bool invertYAxis = false;

	public float score;

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

		instance.invertXAxis = b;
		Debug.Log("m_XAxis : " + SaveOptions.instance.invertXAxis);

		if (player != null)
			player.UpdateXAxis(invertXAxis);
	}

	public void UpdateYAxis(bool b)
	{
		if (player == null)
			player = FindObjectOfType<Player>();

		instance.invertYAxis = b;
		Debug.Log("m_YAxis : " + SaveOptions.instance.invertYAxis);

		if (player != null)
			player.UpdateYAxis(invertYAxis);
	}
}
