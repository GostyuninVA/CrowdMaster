using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState : MonoBehaviour
{
    [SerializeField] private PlayerTransition[] _transitions;

    public Rigidbody Rigidbody { get; private set; }
    protected Animator Animator { get; private set; }

    public void Enter(Rigidbody rigidbody, Animator animator)
    {
        if(enabled == false)
        {
            Rigidbody = rigidbody;
            Animator = animator;

            enabled = true;

            foreach(PlayerTransition transition in _transitions)
            {
                transition.enabled = true;
            }

        }
    }

    public void Exit()
    {
        if(enabled == true)
        {
            foreach(PlayerTransition transition in _transitions)
            {
                transition.enabled = false;
            }

            enabled = false;
        }
    }

    public PlayerState GetNextState()
    {
        foreach(PlayerTransition transition in _transitions)
        {
            if(transition.NeedTransit == true)
            {
                return transition.TargetState;
            }
        }

        return null;
    }
}
