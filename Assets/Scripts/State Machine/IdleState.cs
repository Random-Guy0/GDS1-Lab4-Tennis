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

    }

    public void OnUpdate()
    {
        //if (parameter.range.GetComponent<>().CheckBall) { }
        manager.TransitionState(StateType.Chase);
    }

    public void OnExit() 
    { 
    
    }
}

public class ChaseState : IState
{
    private FSM manager;
    private Parameter parameter;
    private int BallPosition;
    public ChaseState(FSM manager)
    {
        this.manager = manager;
        this.parameter = manager.parameter;
    }

    public void OnEnter()
    {

    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.ball.transform);
        if (parameter.ball)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            parameter.ball.transform.position, parameter.moveSpeed * Time.deltaTime);
        }

        //if (parameter.ball.canHit) { }
        manager.TransitionState(StateType.Hit);
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
        //animation
    }

    public void OnUpdate()
    {
        //animation finish
        //if (!parameter.range.GetComponent<>().CheckBall) { }
        manager.TransitionState(StateType.Idle);
    }

    public void OnExit()
    {

    }
}
