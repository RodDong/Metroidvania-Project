using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    private T stateOwner;
    private IState<T> currentState;
    private IState<T> previousState;
    private IState<T> globalState;

    public StateMachine(T owner)
    {
        stateOwner = owner;
    }

    public IState<T> CurrentState()
    {
        return currentState;
    }

    public IState<T> PreviousState()
    {
        return previousState;
    }

    public IState<T> GlobalState()
    {
        return globalState;
    }

    public void SetCurrentState(IState<T> state)
    {
        currentState = state;
    }

    public void SetPreviousState(IState<T> state)
    {
        previousState = state;
    }

    public void SetGlobalState(IState<T> state)
    {
        globalState = state;
    }

    public void StateMachineUpdate()
    {
        if (globalState != null)
        {
            globalState.Execute(stateOwner);
        }

        if (currentState != null)
        {
            currentState.Execute(stateOwner);
        }
    }

    public void ChangeState(IState<T> newState)
    {
        previousState = currentState;
        if (currentState != null)
            currentState.Exit(stateOwner);
        currentState = newState;
        if (currentState != null)
            currentState.Enter(stateOwner);
    }

    public void Revert2PreviousState()
    {
        ChangeState(previousState);
    }

    public bool IsInState(IState<T> state)
    {
        return currentState == state;
    }
}
