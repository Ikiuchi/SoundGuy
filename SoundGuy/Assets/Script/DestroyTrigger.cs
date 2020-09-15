using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTrigger : MonoBehaviour
{
    public float deathTime = 6f;
    public float currentNbSpirit;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy", deathTime);
    }

    public void SpiritJumpDecrease()
    {
        currentNbSpirit -= 1;
        if (currentNbSpirit == 0)
            Destroy();
    }

	private void Destroy()
	{
        Destroy(gameObject);
	}
}
