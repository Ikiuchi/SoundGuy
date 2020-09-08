using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathProjectile : MonoBehaviour
{
	private Rigidbody rb;
	public float minVelocity = 2;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		Debug.Log(rb.velocity.magnitude);

		if (rb.velocity.magnitude < minVelocity)
		{
			Destroy();
		}
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}
