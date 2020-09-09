using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateform : MonoBehaviour
{
    public float movingSpeed = 5;
    public Vector2 translation = Vector2.zero;

    private Vector3 startingPoint;
    private Vector3 endingPoint;

    bool goingToRight = true;

    private void Start()
    {
        startingPoint = transform.position;
        endingPoint = transform.position + new Vector3(translation.x, translation.y , 0);
    }

    private void Update()
    {
        isGoingToRight();
        Movement();
    }

    private void isGoingToRight()
    {
        float distance = 0;
        if (goingToRight)
            distance = Vector3.Distance(endingPoint, transform.position);
        else
            distance = Vector3.Distance(startingPoint, transform.position);

        if (distance <= 0.2)
            goingToRight = !goingToRight;
    }

    private void Movement()
    {
        if(goingToRight)
            transform.Translate(transform.right * movingSpeed * Time.deltaTime);
        else
            transform.Translate(-transform.right * movingSpeed * Time.deltaTime);
    }
}
