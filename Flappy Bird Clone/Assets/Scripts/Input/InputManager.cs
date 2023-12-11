using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private TouchControls _controls;

    private void Awake()
    {
        _controls = new TouchControls();
    }

    private void OnEnable()
    {
        _controls.Enable();
    }
    
    private void OnDisable()
    {
        _controls.Disable();
    }

    private void Start()
    {
        _controls.Touch.Touch.started += context => StartTouch(context);
    }

    private void StartTouch(InputAction.CallbackContext context)
    {
        Debug.Log("Touch started!");
    }
}
