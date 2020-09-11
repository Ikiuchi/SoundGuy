using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingPlateform : MonoBehaviour
{
    public float fallingTimer = 5;
    public float destroyTimer = 3;
    private float currentTimer;
    private bool updateTimer = false;

    public List<Material> materials;
    private Material currentMaterial;

    bool firstChange = false;
    bool secondChange = false;

    MeshRenderer m;

    void Start()
    {
        currentTimer = fallingTimer;
        m = GetComponent<MeshRenderer>();

        currentMaterial = GetComponent<MeshRenderer>().material;
        GetComponent<MeshRenderer>().material = materials[0];
    }

    // Update is called once per frame
    void Update()
    {
  //      if (updateTimer)
		//{
  //          currentTimer -= Time.deltaTime;
  //          Color c = m.material.color;
  //          c.g -= Time.deltaTime * ((100 / fallingTimer) /100);
  //          m.material.color = c;
  //      }

        if (updateTimer)
        {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= fallingTimer / 3)
			{
                if (!firstChange)
                    GetComponent<MeshRenderer>().material = materials[2]; firstChange = true;
			}
            else if (currentTimer <= (fallingTimer / 3) * 2)
			{
                if (!secondChange)
                    GetComponent<MeshRenderer>().material = materials[1]; secondChange = true;
			}
        }

        if (currentTimer <= 0)
            Fall();
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("PlayerCollision"))
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
