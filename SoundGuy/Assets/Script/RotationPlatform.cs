using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationPlatform : MonoBehaviour
{
    public float rotationVelocity = 0f;
    public float rotationSpeed = 1f;
    new public Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = Quaternion.AngleAxis(rotationVelocity, Vector3.up);
        rigidbody.MoveRotation(Quaternion.AngleAxis(rotationVelocity, Vector3.up));

        rotationVelocity += rotationSpeed * Time.deltaTime;

        if (rotationVelocity >= 360f)
            rotationVelocity = 0f;
    }

    /*private void OnTriggerEnter(Collider other)
    {
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        other.transform.parent = null;
    }*/
}
