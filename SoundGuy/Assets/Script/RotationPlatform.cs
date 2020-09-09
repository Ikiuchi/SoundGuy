using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatform : MonoBehaviour
{
    public float rotationVelocity = 0f;
    public float rotationSpeed = 1f;
    private Rigidbody rigidbody;
    private Quaternion firstRotationValue; 

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        firstRotationValue = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = (Quaternion.AngleAxis(rotationVelocity, transform.up)).normalized;
        //transform.rotation *= firstRotationValue;

        //rigidbody.MoveRotation((Quaternion.AngleAxis(rotationVelocity, transform.up).normalized * firstRotationValue.normalized).normalized);
        transform.rotation = Quaternion.FromToRotation(transform.right, transform.forward);

        rotationVelocity += rotationSpeed * Time.deltaTime;

        if (rotationVelocity >= 360f)
            rotationVelocity = 0f;
        if (rotationVelocity == 180)
            rotationVelocity = 0f;
    }
}
