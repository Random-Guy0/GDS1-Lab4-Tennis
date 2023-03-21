using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallClone : MonoBehaviour
{
    private const float gravity = -9.8f;
    
    private Vector3 velocity;
    private GameObject playerLastHit;

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

        if (transform.position.y - velocity.y * Time.deltaTime <= groundLevel + transform.localScale.y / 2)
        {
            Vector3 position = transform.position;
            position.y = groundLevel + transform.localScale.y/2 ;
            transform.position = position;

            velocity.y = jumpSpeed;
        }
        
        transform.position += velocity * Time.deltaTime;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
        SetTransformY(1.5f);
    }
    public Vector3 GetVelocity()
    {
        return velocity;
    }
    public Vector3 GetDirVelocity()
    {
        return new Vector3(velocity.x, 0, velocity.z);
    }
    public void SetPlayerLastHit(GameObject Player)
    {
        playerLastHit = Player;
    }
    public GameObject GetPlayerLastHit()
    {
        return playerLastHit;
    }
    void SetTransformY(float posy)
    {
        transform.position = new Vector3(transform.position.x, posy, transform.position.z);
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
