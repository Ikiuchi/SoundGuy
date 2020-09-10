using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicMgr : MonoBehaviour
{
	public delegate void MusicUpdate(int i);
	public MusicUpdate musicUpdate;

	//5 level
	public int[] level = new int[5] { 0, 2, 4, 6, 8 };

	public AudioSource[] audio = new AudioSource[5];

	public int currentLvl = 0;

	private void Start()
	{
		musicUpdate += CheckLevel;

		//foreach (AudioSource a in audio)
		//	a.Play();
		
		SwapLevel(0);
	}

	private void CheckLevel(int currentFollower)
	{
		if (currentFollower >= level[4])
			SwapLevel(4);
		else if (currentFollower >= level[3])
			SwapLevel(3);
		else if (currentFollower >= level[2])
			SwapLevel(2);
		else  if (currentFollower >= level[1])
			SwapLevel(1);
		else if (currentFollower >= level[0])
			SwapLevel(0);
	}

	private void SwapLevel(int level)
	{
		currentLvl = level;

		switch (level)
		{
			case 0:
				//audio[1].mute = true;
				//audio[2].mute = true;
				//audio[3].mute = true;
				//audio[4].mute = true;
				break;
			case 1:
				//audio[1].mute = false;
				//audio[2].mute = true;
				//audio[3].mute = true;
				//audio[4].mute = true;
				break;
			case 2:
				//audio[1].mute = false;
				//audio[2].mute = false;
				//audio[3].mute = true;
				//audio[4].mute = true;
				break;
			case 3:
				//audio[1].mute = false;
				//audio[2].mute = false;
				//audio[3].mute = false;
				//audio[4].mute = true;
				break;
			case 4:
				//audio[1].mute = false;
				//audio[2].mute = false;
				//audio[3].mute = false;
				//audio[4].mute = false;
				break;
			default:
				SwapLevel(0);
				break;
		}
	}
}
