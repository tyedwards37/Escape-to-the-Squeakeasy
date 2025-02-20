using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KeyboardInput : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private InputActionReference options;

    private void FixedUpdate()
    {
        Vector2 movementInput = movement.action.ReadValue<Vector2>();

        if (movementInput.y > 0)
        {
            // move up
            Game.Instance.GetHamster().Move(new Vector2(0, 1));
        }
        else if (movementInput.y < 0)
        {
            // move down
            Game.Instance.GetHamster().Move(new Vector2(0, -1));
        }
        else if (movementInput.x < 0)
        {
            // move left
            Game.Instance.GetHamster().Move(new Vector2(-1, 0));
        }
        else if (movementInput.x > 0)
        {
            // move right
            Game.Instance.GetHamster().Move(new Vector2(1, 0));
        }
    }

    private void Update()
    {
        if(options.action.triggered)
        {
            // This needs to be in update because FixedUpdate() is dependant on
            // time which gets set to 0 when paused while Update() is dependant
            // on the user's framerate which is independent of time - Dylan
            Game.Instance.TogglePause();
        }
    }
}
