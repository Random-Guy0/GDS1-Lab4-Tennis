using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    public GameObject ball;
    public Collider targetZone;
    public float speed;
    Vector3 rawInputMovement;
    Rigidbody rb;
    Collider cl;
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
        Vector2 inputMovement = value.ReadValue<Vector2>();
        //MovementInputData = inputMovement;
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        //transform.LookAt(transform.position + rawInputMovement);
        //float speedmod = Mathf.Sqrt(Mathf.Pow(inputMovement.x, 2) + Mathf.Pow(inputMovement.y, 2));
        //anim.SetFloat("Speed", _effectiveSpeed * speedmod);
    }
    void Target()
    {
        // determine firing vector
        Vector3 shootDir = transform.position - ball.transform.position;
        shootDir.y = 0;
        shootDir.Normalize();
        // check target direciton
        Vector3 facingDir = cl.ClosestPoint(targetZone.transform.position);
        facingDir.y = 0;
        facingDir.Normalize();
        // facing targets south side
        if (facingDir.x > -1/Mathf.Sqrt(2) && facingDir.x < 1/Mathf.Sqrt(2) && facingDir.z > 1/Mathf.Sqrt(2) && facingDir.z < 1)
        {
            // Get south bounds
        }
        // facing targets north side
        if (facingDir.x > -1 / Mathf.Sqrt(2) && facingDir.x < 1 / Mathf.Sqrt(2) && facingDir.z < -1 / Mathf.Sqrt(2) && facingDir.z > -1)
        {
            // Get north bounds
        }
        // facing targets west side
        if (facingDir.z > -1 / Mathf.Sqrt(2) && facingDir.z < 1 / Mathf.Sqrt(2) && facingDir.x > 1 / Mathf.Sqrt(2) && facingDir.x < 1)
        {
            // Get west bounds
        }
        // facing targets east side
        if (facingDir.z > -1 / Mathf.Sqrt(2) && facingDir.z < 1 / Mathf.Sqrt(2) && facingDir.x < -1 / Mathf.Sqrt(2) && facingDir.x > -1)
        {
            // Get east bounds
        }
        // check firing vector
        // determin mag
        float ztarget = Random.Range(targetZone.bounds.min.z, targetZone.bounds.max.z);


        float mag = Mathf.Abs(ztarget - transform.position.z) / Mathf.Abs(shootDir.z);

    }

    void Update()
    {
        transform.Translate(rawInputMovement * speed * Time.deltaTime);   
    }
}
