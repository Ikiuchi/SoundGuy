using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlateform : MonoBehaviour
{
    public float fallingTimer = 5;
    public float destroyTimer = 3;
    private float currentTimer;
    private bool updateTimer = false;

    public List<Material> materials;

    MeshRenderer m;

    void Start()
    {
        currentTimer = fallingTimer;
        m = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (updateTimer)
		{
            currentTimer -= Time.deltaTime;
            Color c = m.material.color;
            c.g -= Time.deltaTime * ((100 / fallingTimer) /100);
            m.material.color = c;
        }

        if (currentTimer <= 0)
            Fall();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            updateTimer = true;
    }

    private void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Rigidbody>().useGravity = true;

        Invoke("Destroy", destroyTimer);
    }

    private void Destroy()
	{
        Destroy(gameObject);
	}
}
