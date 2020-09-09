using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PnjController : MonoBehaviour
{
    private Transform player = null;
    public float moveSpeed = 5.0f;
    public float slowingSpeed = 3.0f;
    public float dashPower = 10f;
    public float jumpHeight = 2;
    private bool follow = false;
    private NavMeshAgent navmesh;

    private CapsuleCollider caps;
    private bool dead = false;

    Rigidbody rb;

    Vector3 impulse = Vector3.zero;
    public float impulseValue = 1;

    private Follower f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        navmesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        caps = GetComponent<CapsuleCollider>();

        navmesh.speed = moveSpeed;

        f = FindObjectOfType<Follower>();
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
        if (!follow)
		{
            follow = true;
            f.followerDelegate();
		}
    }

    public void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
            navmesh.speed = slowingSpeed;

        if (other.gameObject.layer == LayerMask.NameToLayer("Jump"))
            Jump();
        else if (other.gameObject.layer == LayerMask.NameToLayer("Dash"))
            Dash();

        if (other.gameObject.layer == LayerMask.NameToLayer("CharmingMusic"))
            Charmed();
    }

	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
            navmesh.speed = moveSpeed;
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
		if (collision.gameObject.layer == LayerMask.NameToLayer("DeathBall"))
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
        dir.Normalize();
        dir.y = 1;
        rb.AddForce(dir.normalized * Mathf.Sqrt(jumpHeight * -1f * Physics.gravity.y), ForceMode.Impulse);
    }

    private void Dash()
	{
        navmesh.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;

        Vector3 dir = player.position - transform.position;
        dir.Normalize();

        rb.AddForce(dir * dashPower / 2, ForceMode.VelocityChange);
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
