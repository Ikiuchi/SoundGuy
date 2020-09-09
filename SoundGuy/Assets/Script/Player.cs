﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController characterController;
    private Rigidbody rigidBody;

    public float moveSpeed = 1f;
    public float slowingSpeed = 0.5f;
    private float currentMoveSpeed;
    public float turnSpeed = 1f;
    public float jumpHeight = 1f;
    public float dashPower = 1f;

    public float rotationSmoothTime = 0.1f; 
    private float rotationSmoothVelocity = 0f; 

    public Transform groundChecker;
    public LayerMask ground;

    private float GroundDistance = 0.2f;

    public float fallingTimer = 2;
    private float currentTimer;

    private Vector3 gravity = new Vector3(0f, 9.81f, 0f);
    private Vector3 movement;

    private bool useDash = false;
    private bool isGrounding;
    public GameObject jumpTrigger;
    public GameObject dashTrigger;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        rigidBody = GetComponent<Rigidbody>();

        currentMoveSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.freezeRotation)
        {
            if (useDash)
            {
                movement = Vector3.zero;
                if (rigidBody.velocity == Vector3.zero)
                    useDash = false;
                return;
            }
            movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

            movement.Normalize();

            if (movement.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(movement.x, movement.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSmoothVelocity, rotationSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);
            }
            isGrounding = Physics.CheckSphere(groundChecker.position, GroundDistance, ground, QueryTriggerInteraction.Ignore);

            if (isGrounding)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    rigidBody.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
                    CreateJumpTrigger();
                }
            }

            if (Input.GetButtonDown("Dash") && !useDash)
            {
                Vector3 dashDirection = movement;
                if (movement == Vector3.zero)
                    dashDirection = transform.forward;

                rigidBody.AddForce(dashDirection * dashPower, ForceMode.VelocityChange);
                useDash = true;
                CreateDashTrigger();
            }
        }

        if (currentTimer > 0)
            currentTimer -= Time.deltaTime;
        else
            GetUp();
    }

    void FixedUpdate()
    {
        if (rigidBody.freezeRotation)
            rigidBody.MovePosition(transform.position + movement * currentMoveSpeed * Time.fixedDeltaTime);
    }

    public void CreateJumpTrigger()
	{
        GameObject g = Instantiate(jumpTrigger, transform.position, Quaternion.identity);
        g.transform.right = transform.forward;
    }
    public void CreateDashTrigger()
    {
        GameObject g = Instantiate(dashTrigger, transform.position, Quaternion.identity);
        g.transform.right = transform.forward;

    }

    private void OnTriggerEnter(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
            currentMoveSpeed = slowingSpeed;
    }
	private void OnTriggerExit(Collider other)
	{
        if (other.gameObject.layer == LayerMask.NameToLayer("Slow"))
            currentMoveSpeed = moveSpeed;
    }

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.layer == LayerMask.NameToLayer("DeathBall"))
            Fall();
	}

	private void Fall()
	{
        currentTimer = fallingTimer;
        rigidBody.freezeRotation = false;
	}

    private void GetUp()
	{
        if (!rigidBody.freezeRotation)
          rigidBody.freezeRotation = true;
    }
}
