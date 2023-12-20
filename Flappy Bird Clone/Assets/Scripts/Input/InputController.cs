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
        public event InputHappened OnInputHappened;
    
        // VARIABLES
        private TouchControls _controls;

        // METHODS
        protected override void AwakeController()
        {
            _controls = new TouchControls();
        }

        protected override void EnableController()
        {
            _controls.Enable();
        }

        protected override void DisableController()
        {
            _controls.Disable();
        }

        protected override void StartController()
        {
            _controls.Input.Jump.started += StartInput;
        }

        private void StartInput(InputAction.CallbackContext context)
        {
            OnInputHappened?.Invoke();
        }
    }
}
