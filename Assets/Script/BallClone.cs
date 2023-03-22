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

    [SerializeField] private AudioSource ballBounceSound;

    private int bounces;
    private float timeSinceLastBounce;

    private void Start()
    {
        velocity = Vector3.zero;
        bounces = 0;
        timeSinceLastBounce = 0;
    }

    private void Update()
    {
        velocity.y += gravity * Time.deltaTime;
        if (velocity.y < terminalVelocity)
        {
            velocity.y = terminalVelocity;
        }

        if (transform.position.y - velocity.y * Time.deltaTime <= groundLevel + transform.localScale.y / 2.0f)
        {
            Vector3 position = transform.position;
            position.y = groundLevel + transform.localScale.y/2 ;
            transform.position = position;

            velocity.y = jumpSpeed;

            ballBounceSound.Play();
            if (playerLastHit != null && timeSinceLastBounce >= 0.5f)
            {
                bounces++;
                timeSinceLastBounce = 0;
            }
        }

        float nextZ = transform.position.z - velocity.z * Time.deltaTime;
        if ((transform.position.z <= 0 && nextZ >= 0) || (transform.position.z >= 0 && nextZ <= 0))
        {
            bounces = 0;
        }
        
        transform.position += velocity * Time.deltaTime;
        timeSinceLastBounce += Time.deltaTime;
    }

    public void SetVelocity(Vector3 newVelocity)
    {
        velocity = newVelocity;
        SetTransformY(1.5f);
        bounces = 0;
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

    public int GetBounces()
    {
        return bounces;
    }

    public void ResetBounces()
    {
        bounces = 0;
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
