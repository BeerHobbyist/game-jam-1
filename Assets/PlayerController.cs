using System;
using System.Collections;
using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float speed;

    private Vector2 _movementInput;
    private Rigidbody _rb;
    
    private void GetMovementInput(Vector2 value) => _movementInput = value;
    
    private void Awake()
    {
        inputReader.OnMovementEvent += GetMovementInput;
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    
    // Only to be used in FixedUpdate
    private void MovePlayer()
    {
        _rb.velocity = new Vector3(_movementInput.x * speed, 0, _movementInput.y * speed);
    }
    
}
