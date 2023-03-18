using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    private const float gravity = -9.8f;
    
    private Vector2 velocity;

    [SerializeField] private float jumpSpeed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float terminalVelocity;

    private void Start()
    {
        velocity = Vector2.zero;
    }

    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        if (velocity.y < terminalVelocity)
        {
            velocity.y = terminalVelocity;
        }
        Debug.Log(velocity);
    }

    private void FixedUpdate()
    {
        rb.position += velocity * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            velocity.y = jumpSpeed;
        }
    }
}
