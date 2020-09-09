using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlateform : MonoBehaviour
{
    public float timer = 2;
    private float currentTimer;
    private bool updateTimer = false;
    // Start is called before the first frame update
    void Start()
    {
        currentTimer = timer;
    }

    // Update is called once per frame
    void Update()
    {

        if (updateTimer)
        {
            Debug.Log(currentTimer);
            currentTimer -= Time.deltaTime;
        }

        if (currentTimer <= 0)
            Fall();

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            updateTimer = true;
    }

    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            updateTimer = false;
            currentTimer = timer;
        }
    }

    private void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
