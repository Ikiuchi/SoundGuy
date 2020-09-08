using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public GameObject ball;
	public float force = 5;

	public void Update()
	{
		if (Input.GetKeyDown(KeyCode.L))
			LaunchBall();
	}

	public void LaunchBall()
	{
		GameObject g = Instantiate(ball, transform.position + new Vector3(0,2,2), Quaternion.identity);
		g.GetComponent<Rigidbody>().AddForce(transform.up * force, ForceMode.Impulse);
	}
}
