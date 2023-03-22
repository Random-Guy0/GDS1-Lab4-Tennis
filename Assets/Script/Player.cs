using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    public BallClone ball;
    public Collider targetZone;
    public float speed;
    Vector3 rawInputMovement;
    Vector3 curTarget;
    bool serving = false;
    Rigidbody rb;
    Collider cl;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void OnMovement(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        //MovementInputData = inputMovement;
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        //transform.LookAt(transform.position + rawInputMovement);
        //float speedmod = Mathf.Sqrt(Mathf.Pow(inputMovement.x, 2) + Mathf.Pow(inputMovement.y, 2));
        //anim.SetFloat("Speed", _effectiveSpeed * speedmod);
    }
    public void OnFire(InputAction.CallbackContext value)
    {
        if (value.started)
        {
            serving = true;
        }
        if (value.canceled)
        {
            serving = false;
        }
    }
    Vector3 Target()
    {
        return new Vector3(Random.Range(targetZone.bounds.min.x, targetZone.bounds.max.x), 0, Random.Range(targetZone.bounds.min.z, targetZone.bounds.max.z));
    }

    void Hit()
    {
        curTarget = Target();
        float distance = Vector3.Distance(ball.transform.position, curTarget);
        Vector3 direction = Vector3.Normalize(curTarget - ball.transform.position);
        Vector3 vel = direction * distance;
        vel.y = 2.1f + distance/10;
        ball.SetVelocity(vel);
        ball.SetPlayerLastHit(gameObject);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.transform.name == ball.name && ball.GetPlayerLastHit() != gameObject)
        {
            Debug.Log(ball.GetDirVelocity());
            if (ball.GetDirVelocity().magnitude <= 0.1f && serving)
            {
                Hit();
            }
            else if (ball.GetDirVelocity().magnitude > 0.1f)
            {
                Hit();
            } 
        }
    }

    void Update()
    {
        transform.Translate(rawInputMovement * speed * Time.deltaTime);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(curTarget, 0.5f);
    }
    public float GetInputMagnitude()
    {
        return rawInputMovement.magnitude;
    }
    public float GetCurrentSpeed()
    {
        return rawInputMovement.magnitude * speed;
    }
    //void Target()
    //{
    //    // determine firing vector
    //    Vector3 shootDir = transform.position - ball.transform.position;
    //    shootDir.y = 0;
    //    shootDir.Normalize();
    //    // check target direciton
    //    Vector3 facingDir = cl.ClosestPoint(targetZone.transform.position);
    //    facingDir.y = 0;
    //    facingDir.Normalize();
    //    // facing targets south side
    //    if (facingDir.x > -1 / Mathf.Sqrt(2) && facingDir.x < 1 / Mathf.Sqrt(2) && facingDir.z > 1 / Mathf.Sqrt(2) && facingDir.z < 1)
    //    {
    //        // Get south bounds
    //    }
    //    // facing targets north side
    //    if (facingDir.x > -1 / Mathf.Sqrt(2) && facingDir.x < 1 / Mathf.Sqrt(2) && facingDir.z < -1 / Mathf.Sqrt(2) && facingDir.z > -1)
    //    {
    //        // Get north bounds
    //    }
    //    // facing targets west side
    //    if (facingDir.z > -1 / Mathf.Sqrt(2) && facingDir.z < 1 / Mathf.Sqrt(2) && facingDir.x > 1 / Mathf.Sqrt(2) && facingDir.x < 1)
    //    {
    //        // Get west bounds
    //    }
    //    // facing targets east side
    //    if (facingDir.z > -1 / Mathf.Sqrt(2) && facingDir.z < 1 / Mathf.Sqrt(2) && facingDir.x < -1 / Mathf.Sqrt(2) && facingDir.x > -1)
    //    {
    //        // Get east bounds
    //    }
    //    // check firing vector
    //    // determin mag
    //    float ztarget = Random.Range(targetZone.bounds.min.z, targetZone.bounds.max.z);


    //    float mag = Mathf.Abs(ztarget - transform.position.z) / Mathf.Abs(shootDir.z);

    //}
}
