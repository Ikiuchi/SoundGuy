using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>())
            SceneManager.LoadScene("VictoryScene");
    }
}
