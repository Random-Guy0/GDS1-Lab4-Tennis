using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class Player : MonoBehaviour
{
    public float speed;
    Vector3 rawInputMovement;
    Rigidbody rb;
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

    void Update()
    {
        transform.Translate(rawInputMovement * speed * Time.deltaTime);   
    }
}
