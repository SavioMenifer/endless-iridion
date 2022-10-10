using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectCollisions : MonoBehaviour
{
    public GameObject gameManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "spike")
        {
            gameManager.GetComponent<GameManager>().GameOver();
            Debug.Log("collided with spike");
        }
        else if (other.tag == "orb")
        {
            gameManager.GetComponent<GameManager>().OrbCollected();
            Destroy(other.gameObject);
            Debug.Log("collided with orb");
        }
    }
}
