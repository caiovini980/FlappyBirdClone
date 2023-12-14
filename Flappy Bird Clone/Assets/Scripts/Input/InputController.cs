using Base.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputController : ControllerBase
    {
        // DELEGATES
        public delegate void InputHappened();

        // EVENTS
        public static event InputHappened OnInputHappened;
    
        // VARIABLES
        private TouchControls _controls;

        // METHODS
        protected override void AwakeController()
        {
            Debug.Log("Creating new input controls...");
            _controls = new TouchControls();
        }

        protected override void EnableController()
        {
            Debug.Log("Enabling input controls...");
            _controls.Enable();
        }

        protected override void DisableController()
        {
            Debug.Log("Disabling input controls...");
            _controls.Disable();
        }

        protected override void StartController()
        {
            Debug.Log("Setting events for Input controls...");
            _controls.Input.Jump.started += StartInput;
        }

        private void StartInput(InputAction.CallbackContext context)
        {
            OnInputHappened?.Invoke();
        }
    }
}
