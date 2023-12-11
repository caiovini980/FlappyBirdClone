using Base.Controllers;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class InputController : ControllerBase
    {
        // DELEGATES
        public delegate void TouchHappened();

        // EVENTS
        public event TouchHappened OnTouchHappened;
    
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
            _controls.Touch.Touch.started += StartTouch;
        }

        private void StartTouch(InputAction.CallbackContext context)
        {
            Debug.Log("Touch started!");
            if (OnTouchHappened != null) { OnTouchHappened(); }
        }
    }
}
