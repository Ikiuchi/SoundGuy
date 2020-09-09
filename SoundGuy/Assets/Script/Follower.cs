using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follower : MonoBehaviour
{
    public Text textFollower;

	public delegate void FollowerDelegate();
	public FollowerDelegate followerDelegate;

    public int nbFollower = 0;

    MusicMgr musicMgr;

    void Start()
    {
        followerDelegate = UpdateFollower;
        textFollower.text = nbFollower.ToString();

        musicMgr = FindObjectOfType<MusicMgr>();
    }

    void UpdateFollower()
    {
        nbFollower++;
        textFollower.text = nbFollower.ToString();

        if (musicMgr)
            musicMgr.musicUpdate(nbFollower);
    }
}
