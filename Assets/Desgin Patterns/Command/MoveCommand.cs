using UnityEngine;

public class MoveCommand : ICommand
{
    CharacterController characterController;
    private Vector3 movement;

    public MoveCommand(CharacterController characterController, Vector3 movement)
    {
        this.characterController = characterController;
        this.movement = movement;
    }
    
    public void Execute()
    {
        Vector3 moveToApply = new Vector3(movement.x, 0 , movement.y);
        characterController.Move(moveToApply * Time.deltaTime);
    }

    public void Undo()
    {
        characterController.Move(-movement);
    }
}
