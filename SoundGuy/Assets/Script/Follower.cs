using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Follower : MonoBehaviour
{
    public Text textFollower;

	public delegate void FollowerDelegate();
	public FollowerDelegate followerDelegate;
	public FollowerDelegate followerMinus;

    public int nbFollower = 0;

    MusicMgr musicMgr;

    private AudioSource newFollowerSound;

    void Start()
    {
        followerDelegate = UpdateFollower;
        followerMinus = DecreaseFollower;

        textFollower.text = nbFollower.ToString();

        musicMgr = FindObjectOfType<MusicMgr>();

        newFollowerSound = GetComponent<AudioSource>();
    }

    void UpdateFollower()
    {
        newFollowerSound.Play();
        nbFollower++;
        textFollower.text = nbFollower.ToString();

        if (musicMgr)
            musicMgr.musicUpdate(nbFollower);
    }

    void DecreaseFollower()
    {
        nbFollower--;
        textFollower.text = nbFollower.ToString();

        if (musicMgr)
            musicMgr.musicUpdate(nbFollower);
    }

    private void OnDestroy()
    {
        if(SaveOptions.instance != null)
            SaveOptions.instance.score = nbFollower;
    }
}
