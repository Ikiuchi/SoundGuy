using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PnjController : MonoBehaviour
{
    public Transform player = null;
    public float moveSpeed = 5.0f;
    public bool follow = true;
    public NavMeshAgent navmesh;

    public CapsuleCollider caps;
    private bool dead = false;

    Rigidbody rb;

    Vector3 impulse = Vector3.zero;
    public float impulseValue = 1;

    void Start()
    {
        navmesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        caps = GetComponent<CapsuleCollider>();

        navmesh.speed = moveSpeed;
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
        if (dead)
            return;

        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
		{
            navmesh.enabled = true;
            rb.isKinematic = true;
            rb.useGravity = false;
		}
		else if (collision.gameObject.layer == LayerMask.NameToLayer("DeathBall"))
        {
            impulse = collision.impulse;
            Death();
        }
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

    private void Death()
	{
        dead = true;
        navmesh.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;

        rb.AddForce(-impulse * impulseValue, ForceMode.Impulse);

        Invoke("Destroy", 5.0f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
