using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathProjectile : MonoBehaviour
{
	private Rigidbody rb;
	public float minVelocity = 5;
    public float timer;
    private float currentTimer;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
        currentTimer = 0;

    }

	private void Update()
	{
        currentTimer += Time.deltaTime;

        //if (rb.velocity.magnitude < 1 && rb.velocity.magnitude != 0)
        if (currentTimer > timer)
			Destroy();
		else if (rb.velocity.magnitude < minVelocity && rb.velocity.magnitude != 0)
			gameObject.layer = LayerMask.NameToLayer("Default");
	}

	private void Destroy()
	{
		Destroy(gameObject);
	}
}
