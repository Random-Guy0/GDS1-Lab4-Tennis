using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShadow : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private float groundLevel;

    private void LateUpdate()
    {
        Vector3 ballPos = ball.transform.position;
        ballPos.y = groundLevel;
        transform.position = ballPos;
    }
}
