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
        if (parameter.range.GetComponent<RangeCheck>().GetBallCheck()) 
        {
            manager.TransitionState(StateType.Chase);
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

    }

    public void OnUpdate()
    {
        manager.FlipTo(parameter.ball.transform);
        Vector2 ball = new Vector2(parameter.ball.transform.position.x, manager.transform.position.y);
        if (parameter.ball)
        {
            manager.transform.position = Vector2.MoveTowards(manager.transform.position,
            ball, parameter.moveSpeed * Time.deltaTime);
        }

        if(Physics2D.OverlapCircle(parameter.attackPoint.position, parameter.attackArea, parameter.targetLayer))
        {
            manager.TransitionState(StateType.Hit);
        }

        //if (parameter.ball.canHit) { }
        //manager.TransitionState(StateType.Hit);
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
        if (parameter.range.GetComponent<RangeCheck>().GetBallCheck() == false)
        {
            manager.TransitionState(StateType.Idle);
        }
    }

    public void OnExit()
    {

    }
}
