using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Input
{
    //[CreateAssetMenu(fileName = "InputReader", menuName = "Input/InputReader")]
    public class InputReader : ScriptableObject, GameInput.IGameplayActions
    {
        public UnityAction<Vector2> OnMovementEvent;
        
        private GameInput _gameInput;
        
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
            OnMovementEvent?.Invoke(context.ReadValue<Vector2>());
        }
    }
}
