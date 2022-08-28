using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseWander : IState<Mouse>
{
    public static MouseWander Instance { get; private set; }

    static MouseWander()
    {
        Instance = new MouseWander();
    }

    public void Enter(Mouse enemy)
    {
        enemy.animator.Play("Wander");
    }

    public void Execute(Mouse enemy)
    {
        enemy.Wander();

        if (!enemy.CanWander())
        {
            enemy.GetStateMachine().ChangeState(MouseIdle.Instance);
        }

    }

    public void Exit(Mouse enemy)
    {

    }
}



public class MouseIdle : IState<Mouse>
{
    public static MouseIdle Instance { get; private set; }

    static MouseIdle()
    {
        Instance = new MouseIdle();
    }

    public void Enter(Mouse enemy)
    {
        enemy.animator.Play("Idle");
    }

    public void Execute(Mouse enemy)
    {

    }

    public void Exit(Mouse enemy)
    {

    }
}