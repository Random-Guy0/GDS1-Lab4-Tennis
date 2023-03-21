using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IState
{
    private FSM manager;
    private Parameter parameter;

    public IdleState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("IDLE");
    }

    public void OnUpdate()
    {
        Vector3 back = new Vector3(manager.transform.position.x, 1f, 8f);
        manager.transform.position = Vector3.MoveTowards(manager.transform.position,
            back, parameter.moveSpeed * Time.deltaTime);
        if (parameter.chaseRange.GetComponent<RangeCheck>().GetBallCheck()) 
        {
            manager.TransitionState(StateType.Chase);
        }
        if (parameter.hitRange.GetComponent<RangeCheck>().GetBallCheck())
        {
            manager.TransitionState(StateType.Attack);
        }
    }

    public void OnExit() 
    { 

    }
}

public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("Chase");
    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.ball.transform);
        Vector3 ball = new Vector3(parameter.ball.transform.position.x, manager.transform.position.y, 8);
        if (parameter.ball)
        {
            manager.transform.position = Vector3.MoveTowards(manager.transform.position,
            ball, parameter.moveSpeed * Time.deltaTime);
        }

        if (parameter.hitRange.GetComponent<RangeCheck>().GetBallCheck())
        {
            manager.TransitionState(StateType.Attack);
        }

        if (!parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }
        if (parameter.hitRange.GetComponent<RangeCheck>().GetBallCheck() == false)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {
        
    }
}

public class HitState : IState
{
    private FSM manager;
    private Parameter parameter;


    public HitState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("hit");
    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.ball.transform);
        if (parameter.hitRange.GetComponent<RangeCheck>().GetBallCheck() == false)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {

    }
}

public class AttackState : IState
{
    private FSM manager;
    private Parameter parameter;

    public AttackState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {
        Debug.Log("attack");
    }

    public void OnUpdate()
    {
        Vector3 ball = new Vector3(parameter.ball.transform.position.x, 1, parameter.ball.transform.position.z);
        manager.transform.position = Vector3.MoveTowards(manager.transform.position,
            parameter.ball.transform.position, parameter.moveSpeed * Time.deltaTime);
        if (!parameter.isHit)
        {
            manager.TransitionState(StateType.Hit);
        }
    }

    public void OnExit()
    {
        parameter.isHit = true;
    }
}
