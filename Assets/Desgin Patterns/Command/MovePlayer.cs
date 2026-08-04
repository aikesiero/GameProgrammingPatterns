using UnityEngine;
using UnityEngine.InputSystem;

public class MovePlayer : MonoBehaviour
{
    [SerializeField]
    CharacterController characterController;

    [SerializeField]
    InputActionReference inputActionMove;

    private Vector2 rawMovement;
    
    void OnEnable()
    {
        inputActionMove.action.Enable();

        inputActionMove.action.started += OnMove;
        inputActionMove.action.performed += OnMove;
        inputActionMove.action.canceled += OnMove;
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        rawMovement = context.ReadValue<Vector2>();
    }

    void Update()
    {
        ICommand command = new MoveCommand(characterController, rawMovement);
        CommandInvoker.ExecuteCommand(command);
    }

    void OnDisable()
    {
        inputActionMove.action.Disable();
        
        inputActionMove.action.started -= OnMove;
        inputActionMove.action.performed -= OnMove;
        inputActionMove.action.canceled -= OnMove;
    }
}
