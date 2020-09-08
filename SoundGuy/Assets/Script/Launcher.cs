using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject ball;
	public float force = 5;

	public float launchTimer = 5;

	private void Start()
	{
		InvokeRepeating("LaunchProjectile", 1.0f, launchTimer);
	}

	public void LaunchProjectile()
	{
		GameObject g = Instantiate(ball, transform.position + new Vector3(0,1,1), Quaternion.identity);
		g.GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Impulse);
	}
}
