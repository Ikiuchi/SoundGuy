using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    private CharacterController characterController;

    public float moveSpeed = 1f;
    public float turnSpeed = 1f;
    public float jumpHeight = 1f;
    public float dashPower = 1f;


    private Vector3 gravity = new Vector3(0f, 9.81f, 0f);
    private Vector3 velocity;


    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        characterController.SimpleMove(movement * moveSpeed);

        if(characterController.isGrounded)
        {
            velocity = Vector3.zero;

            if(Input.GetKeyDown("space"))
            {
                 velocity = new Vector3(0f, jumpHeight, 0f) ;
            }

            if(Input.GetButtonDown("Dash"))
            {
                velocity = transform.forward * dashPower;
            }
        }

        velocity -= gravity * Time.deltaTime;

        characterController.Move(velocity * Time.deltaTime);

    }
}
