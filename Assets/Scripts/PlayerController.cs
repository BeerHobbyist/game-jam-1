using System;
using System.Collections;
using Input;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;

    private Vector2 _movementInput;
    private bool _dashInput;
    private Rigidbody _rb;
    private float _timeSinceLastDash;
    private Vector2 _dashDirection;
    private float _dashTime;
    
    private void GetMovementInput(Vector2 value) => _movementInput = value;
    private void GetDashInput(bool flag) => _dashInput = flag;
    
    private void Awake()
    {
        inputReader.onMovementEvent += GetMovementInput;
        inputReader.onDashEvent += GetDashInput;
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        MovePlayer();
    }
    
    // Only to be used in FixedUpdate
    private void MovePlayer()
    {
        if(_timeSinceLastDash > dashCooldown && _dashInput)
        {
            Dash();
            return;
        }
        _rb.velocity = new Vector3(_movementInput.x * speed, 0, _movementInput.y * speed);
        _timeSinceLastDash += Time.deltaTime;
        _dashDirection = _movementInput;
    }

    private void Dash()
    {

        if (_dashTime < dashDuration)
        {
            _rb.velocity = new Vector3(_dashDirection.x * dashSpeed, 0, _dashDirection.y * dashSpeed);
            _dashTime += Time.deltaTime;
        }
        else
        {
            _dashTime = 0;
            _timeSinceLastDash = 0;
        }
        
    }
    
    
}
