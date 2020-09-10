using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public float movingSpeed = 5;
    public Transform translation;
    public float timerStop = 1f;

    private float currentTimer = 0;

    private Vector3 startingPoint;
    private Vector3 endingPoint;

    Vector3 direction;

    bool goingToRight = true;

    private void Start()
    {
        startingPoint = transform.position;
        endingPoint = translation.position;

        direction = (endingPoint - startingPoint).normalized;
    }

    private void Update()
    {
        isGoingToRight();
        Movement();
        if (currentTimer > 0)
            currentTimer -= Time.deltaTime;
    }

    private void isGoingToRight()
    {
        float distance;
        if (goingToRight)
            distance = Vector3.Distance(endingPoint, transform.position);
        else
            distance = Vector3.Distance(startingPoint, transform.position);

        if (distance <= 0.2)
        {
            currentTimer = timerStop;
            goingToRight = !goingToRight;
        }
       
    }

    private void Movement()
    {
        if (currentTimer <= 0)
        {
            if (goingToRight)
            {
                transform.position = transform.position + (direction * movingSpeed * Time.deltaTime);
                //GetComponent<Rigidbody>().MovePosition(transform.position + (transform.right * movingSpeed * Time.deltaTime));
            }
            else
                transform.position = transform.position + (-direction * movingSpeed * Time.deltaTime);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            collision.transform.parent = transform;
        }
        else if (collision.gameObject.GetComponent<PnjController>() != null)
        {
            collision.transform.parent = transform;
        }
    }
    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            collision.transform.parent = null;
        }
        else if (collision.gameObject.GetComponent<PnjController>() != null)
        {
            collision.transform.parent = null;
        }

    }
}
