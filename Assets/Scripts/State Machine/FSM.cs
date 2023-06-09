using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateType
{
    Idle,Chase,Hit,Attack
}

[Serializable]
public class Parameter
{
    public float moveSpeed;
    public GameObject ball;
    public GameObject chaseRange;
    public GameObject hitRange;
    public LayerMask targetLayer;
    public Transform attackPoint;
    public float attackArea;
    public bool isHit;
}

public class FSM : MonoBehaviour
{
    public Parameter parameter = new Parameter();

    public IState currentState;
    public Animator animator;
    private Dictionary<StateType, IState> states = new Dictionary<StateType, IState>();
    // Start is called before the first frame update
    void Start()
    {
        parameter.ball = GameObject.Find("Ball");
        parameter.chaseRange = GameObject.Find("ChaseRange");
        parameter.hitRange = GameObject.Find("HitRange");
        states.Add(StateType.Idle, new IdleState(this));
        states.Add(StateType.Chase, new ChaseState(this));
        states.Add(StateType.Hit, new HitState(this));
        states.Add(StateType.Attack, new AttackState(this));
        parameter.isHit = true;

        TransitionState(StateType.Idle);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.OnUpdate();
    }

    public void TransitionState(StateType type)
    {
        if(currentState != null)
        {
            currentState.OnExit();
        }
        if (type != StateType.Idle)
        {
            animator.SetBool("IsRun", true);
        }
        else if (type == StateType.Idle)
        {
            animator.SetBool("IsRun", false);
        }
        currentState = states[type];
        currentState.OnEnter();
    }

    public void FlipTo(Transform target)
    {
        if(target != null)
        {
            if(transform.position.x > target.position.x)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            else if(transform.position.x < target.position.x)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(parameter.attackPoint.position, parameter.attackArea);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Ball")
        {
            parameter.isHit = false;
        }
    }
}
