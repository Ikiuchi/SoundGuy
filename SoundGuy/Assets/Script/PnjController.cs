﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PnjController : MonoBehaviour
{
    private Transform player = null;
    private Transform lastParent;
    public float moveSpeed = 5.0f;
    public float slowingSpeed = 3.0f;
    public float jumpHeight = 2;
    private bool follow = false;
    private bool isDashing = false;
    private bool endDashing = false;
    private bool isGrounded = true;
    private bool isJumping = false;
    private NavMeshAgent navmesh;

    private CapsuleCollider caps;
    private bool dead = false;
    private bool onPlateform = false;

    public float dashPower = 50f;
    public float dashDistance = 5f;
    [Range(0.01f, 0.2f)]
    public float YDashDirection = 0.05f;
    public float timerDash;
    private float currentTimerDash;
    private Vector3 lastPosDash;

    public Transform groundChecker;
    public LayerMask ground;
    private float GroundDistance = 0.3f;

    Rigidbody rb;

    Vector3 impulse = Vector3.zero;
    public float impulseValue = 1;

    private Follower f;
    private Player pl;

    public AudioSource hitSource;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pl = FindObjectOfType<Player>();

        navmesh = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        caps = GetComponent<CapsuleCollider>();

        navmesh.speed = moveSpeed;

        f = FindObjectOfType<Follower>();

        pl.playerJump = Jump;

        lastParent = transform.parent;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundChecker.position, GroundDistance, ground, QueryTriggerInteraction.Ignore);
        if (isDashing)
        {
            currentTimerDash += Time.deltaTime;

            if (currentTimerDash >= timerDash && !endDashing)
            {
                currentTimerDash = 0f;
                //rb.velocity = transform.forward * 10f;
                endDashing = true;
            }
            if (isGrounded && endDashing)
            {
                ActiveNavMesh();
                isDashing = false;
                endDashing = false;
            }
            else
                return;
        }

        if (follow && !isJumping)
        {
            Collider[] collider = Physics.OverlapSphere(groundChecker.position, GroundDistance, ground, QueryTriggerInteraction.Ignore);
            if (collider.Length == 0f)
            {
                UnActiveNavMesh();
                isJumping = true;
            }
        }
        else if (follow && isGrounded)
            isJumping = false;


        if (follow)
            Movement();
    }

    public void Movement()
	{
        if (onPlateform)
            MovementOnPlateform();
        else if (navmesh.enabled && player != null)
            navmesh.destination = player.position;
    }

    public void Charmed()
	{
        if (!follow && !dead)
		{
            follow = true;
            f.followerDelegate();
            pl.AnimationCharm();
        }
    }

    public void UnCharmed()
    {
        if (follow)
        {
            follow = false;
            f.followerMinus();
        }
    }

    public void UnActiveNavMesh()
	{
        navmesh.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = true;

        rb.freezeRotation = true;
        rb.WakeUp();
    }

    public void ActiveNavMesh()
    {
        navmesh.enabled = true;
        rb.isKinematic = true;
        rb.useGravity = false;

        rb.freezeRotation = false;
        onPlateform = false;
        isJumping = false;
    }

    public void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
            navmesh.speed = slowingSpeed;

        if (other.gameObject.layer == LayerMask.NameToLayer("Jump"))
        {
            Jump();
            other.GetComponent<DestroyTrigger>().SpiritJumpDecrease();
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Dash"))
        {
            Dash();
            other.GetComponent<DestroyTrigger>().SpiritJumpDecrease();
            if(!isGrounded)
                other.GetComponent<DestroyTrigger>().SpiritJumpDecrease();
        }

        if (other.gameObject.layer == LayerMask.NameToLayer("CharmingMusic"))
            Charmed();

        if (other.gameObject.layer == LayerMask.NameToLayer("Water") && !isJumping)
        {
            onPlateform = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
            navmesh.speed = moveSpeed;
    }

	public void OnCollisionEnter(Collision collision)
	{
        if (dead)
            return;

        if ((collision.gameObject.layer == LayerMask.NameToLayer("Ground") || collision.gameObject.layer == LayerMask.NameToLayer("Default")) && (!isJumping && !isDashing))
            ActiveNavMesh();
        if (collision.gameObject.layer == LayerMask.NameToLayer("DeathBall"))
        {
            impulse = collision.impulse;
            Death();
        }
    }

    private void MovementOnPlateform()
	{
        Vector3 dir = player.position - transform.position;
        dir.y = 0f;

        if(dir.magnitude > 1f && !isJumping)
            transform.position += dir.normalized * moveSpeed * Time.deltaTime;
	}

	private void Jump()
	{
        if (follow && !isJumping)
		{
            if(rb.useGravity == false)
                UnActiveNavMesh();
            if (transform.parent != lastParent)
                transform.parent = lastParent;
            Vector3 dir = player.position - transform.position;
            dir.Normalize();
            dir.y = 1;
            dir.Normalize();
            rb.velocity = Vector3.zero;
            rb.AddForce(dir.normalized * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
            /*if(onPlateform)
                rb.AddForce(dir.normalized * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);*/
            isJumping = true;
            onPlateform = false;

        }
    }

    private void Dash()
	{
        UnActiveNavMesh();

        Vector3 dashDirection = player.position - transform.position;
        dashDirection.y = YDashDirection;
        dashDirection.Normalize();
        rb.velocity = Vector3.zero;
        rb.AddForce(dashDirection * dashPower, ForceMode.VelocityChange);
        currentTimerDash = 0f;

        lastPosDash = transform.position;
        isDashing = true;
    }

    private void Death()
	{
        UnCharmed();
        dead = true;
        navmesh.enabled = false;
        rb.isKinematic = false;
        rb.WakeUp();
        rb.useGravity = true;
        rb.freezeRotation = false;

        rb.AddForce(-impulse * impulseValue, ForceMode.Impulse);

        hitSource.Play();

        Invoke("Destroy", 5.0f);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

}
