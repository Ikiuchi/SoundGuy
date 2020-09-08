using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PnjController : MonoBehaviour
{
    public Transform player = null;
    public float moveSpeed = 10.0f;
    public bool follow = true;
    public NavMeshAgent navmesh;

    public CapsuleCollider caps;

    Rigidbody rb;

    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        caps = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        if (follow)
            Movement();
    }

    public void Movement()
	{
        if (navmesh.enabled)
            navmesh.destination = player.position;
    }

    public void Charmed()
	{
        follow = true;
	}

	public void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Jump"))
            Jump();
        else if (other.gameObject.layer == LayerMask.NameToLayer("Dash"))
            Dash();
    }

	public void OnCollisionEnter(Collision collision)
	{
        navmesh.enabled = true;
        rb.isKinematic = true;
        rb.useGravity = false;
    }

	private void Jump()
	{
        navmesh.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;

        Vector3 dir = player.position - transform.position;

        rb.AddForce(dir.x * 3, Vector3.up.y * 100, dir.z * 3);
    }

    private void Dash()
	{
        Debug.Log("Dash");
    }


}
