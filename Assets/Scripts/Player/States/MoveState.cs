using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : PlayerState
{
    [SerializeField] private StaminaAccumulator _staminaAccumulator;

    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedRatio;

    private void OnEnable()
    {
        _playerInput.DirectionChange += OnDirectionChanged;
        _staminaAccumulator.StartAccumulate();
    }

    private void OnDisable()
    {
        _playerInput.DirectionChange -= OnDirectionChanged;
        Animator.SetFloat("run", 0);
    }

    private void OnDirectionChanged(Vector2 direction)
    {
        Rigidbody.velocity = new Vector3(direction.x, 0, direction.y) * _speedRatio;

        if(Rigidbody.velocity.magnitude > _speed)
        {
            Rigidbody.velocity *= _speed / Rigidbody.velocity.magnitude;
        }

        if(Rigidbody.velocity.magnitude != 0)
        {
            Rigidbody.MoveRotation(Quaternion.LookRotation(Rigidbody.velocity, Vector3.up));
        }
    }

    private void Update()
    {
        Animator.SetFloat("run", Rigidbody.velocity.magnitude);
        Debug.Log(Rigidbody.velocity.magnitude);
    }
}
