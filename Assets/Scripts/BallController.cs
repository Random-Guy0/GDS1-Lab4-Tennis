using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float gravity = -9.8f;
    
    private Vector3 velocity;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private float terminalVelocity;
    [SerializeField] private float groundLevel;

    private void Start()
    {
        velocity = Vector3.zero;
    }

    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        if (velocity.y < terminalVelocity)
        {
            velocity.y = terminalVelocity;
        }

        if (transform.position.y - velocity.y * Time.deltaTime <= groundLevel)
        {
            Vector3 position = transform.position;
            position.y = groundLevel;
            transform.position = position;

            velocity.y = jumpSpeed;
        }

        velocity.x = 3;
        velocity.z = 3;
        
        transform.position += velocity * Time.deltaTime;
    }

    /*
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            velocity.y = jumpSpeed;
        }
    }
    */
}
