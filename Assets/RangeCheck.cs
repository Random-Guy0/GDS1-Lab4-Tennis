using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeCheck : MonoBehaviour
{
    public bool BallIn;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ball")
        {
            BallIn = true;
            Debug.Log("InS");
        }
    }

    public bool GetBallCheck()
    {
        return BallIn;
    }
}
