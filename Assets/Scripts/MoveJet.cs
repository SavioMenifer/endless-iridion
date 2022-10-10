using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJet : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody jet;
    private float velocityX,
        velocityY;

    void Start()
    {
        jet = gameObject.GetComponent<Rigidbody>();
    }

    void Update()
    {
        velocityX = Input.GetAxis("Horizontal") * moveSpeed;
        velocityY = Input.GetAxis("Vertical") * moveSpeed;
    }

    void FixedUpdate()
    {
        jet.velocity = new Vector3(velocityX, velocityY, jet.velocity.z);
    }
}
