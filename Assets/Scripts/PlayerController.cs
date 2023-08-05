using Input;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    private enum PlayerState
    {
        Walking,
        Dashing
    }
    
    [SerializeField] private InputReader inputReader;
    [SerializeField] private float speed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;

    private Vector2 _movementInput;
    private bool _dashInput;
    private Rigidbody _rb;
    private PlayerState _playerState;
    private float _dashTimer;
    
    private void GetMovementInput(Vector2 value) => _movementInput = value;
    private void GetDashInput(bool flag) => _dashInput = flag;
    
    private void Awake()
    {
        inputReader.onMovementEvent += GetMovementInput;
        inputReader.onDashEvent += GetDashInput;
        _rb = GetComponent<Rigidbody>();
        _playerState = PlayerState.Walking;
    }

    private void FixedUpdate()
    {
        _playerState = _dashInput ? PlayerState.Dashing : PlayerState.Walking;

        MovePlayer();
    }
    
    // Only to be used in FixedUpdate
    private void MovePlayer()
    {
        if (_playerState == PlayerState.Walking)
        {
            _rb.velocity = new Vector3(_movementInput.x * speed, 0, _movementInput.y * speed);
            return;
        }
        Dash();
        _dashTimer += Time.deltaTime;
        
        if (_dashTimer < dashDuration)
            return;
        
        _playerState = PlayerState.Walking;
        _dashTimer = 0f;
    }

    private void Dash()
    {
        _rb.velocity = new Vector3(_movementInput.x * dashSpeed, 0, _movementInput.y * dashSpeed);
    }
    
}
