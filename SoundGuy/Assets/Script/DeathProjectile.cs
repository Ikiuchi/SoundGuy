using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathProjectile : MonoBehaviour
{
	private Rigidbody rb;
	public float minVelocity = 5;
	private void Start()
	{
		rb = GetComponent<Rigidbody>();
	}

	private void Update()
	{

		if (rb.velocity.magnitude < 1 && rb.velocity.magnitude != 0)
			Destroy();
		else if (rb.velocity.magnitude < minVelocity && rb.velocity.magnitude != 0)
			gameObject.layer = LayerMask.NameToLayer("Default");
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}
