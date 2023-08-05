using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    //[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions
    {
        public UnityAction<Vector2> onMovementEvent;
        public UnityAction<Vector2> onMousePositionEvent;
        public UnityAction<bool> onDashEvent;
        public UnityAction onShootEvent;

        private GameInput _gameInput;

        public InputReader(UnityAction<Vector2> onMousePositionEvent)
        {
            this.onMousePositionEvent = onMousePositionEvent;
        }

        private void OnEnable()
        {
            if (_gameInput == null)
            {
                _gameInput = new GameInput();
                _gameInput.Gameplay.SetCallbacks(this);
            }
            
            _gameInput.Gameplay.Enable();
        }

        public void OnMovement(InputAction.CallbackContext context)
        {
            onMovementEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnMousePosition(InputAction.CallbackContext context)
        {
            onMousePositionEvent?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnShoot(InputAction.CallbackContext context)
        {
            if(InputActionPhase.Performed == context.phase)
                onShootEvent?.Invoke();
        }

        public void OnDash(InputAction.CallbackContext context)
        {
            onDashEvent?.Invoke(context.phase == InputActionPhase.Performed);
        }
    }
}
