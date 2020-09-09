using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    public float deathTime = 15;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", deathTime);
    }

	private void Destroy()
	{
        Destroy(gameObject);
	}
}
