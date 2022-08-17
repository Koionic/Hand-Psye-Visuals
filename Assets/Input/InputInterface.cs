using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputInterface : MonoBehaviour
{
    public UnityEvent<InputAction.CallbackContext> face_up;
    public UnityEvent<InputAction.CallbackContext> face_down;
    public UnityEvent<InputAction.CallbackContext> face_left;
    public UnityEvent<InputAction.CallbackContext> face_right;
    
    public UnityEvent<InputAction.CallbackContext> shoulder_left;
    public UnityEvent<InputAction.CallbackContext> shoulder_right;
    
    public UnityEvent<InputAction.CallbackContext> trigger_left;
    public UnityEvent<InputAction.CallbackContext> trigger_right;

    public UnityEvent<InputAction.CallbackContext> dpad_up;
    public UnityEvent<InputAction.CallbackContext> dpad_down;
    public UnityEvent<InputAction.CallbackContext> dpad_left;
    public UnityEvent<InputAction.CallbackContext> dpad_right;
    
    public UnityEvent<InputAction.CallbackContext> leftStick_x;
    public UnityEvent<InputAction.CallbackContext> leftStick_y;
    
    public UnityEvent<InputAction.CallbackContext> rightStick_x;
    public UnityEvent<InputAction.CallbackContext> rightStick_y;

    public void OnControllerConnect(PlayerInput playerInput)
    {
        Debug.Log("CONTROLLER CONNECTED");
    }
    
    public void OnControllerDisconnect(PlayerInput playerInput)
    {
        Debug.Log("CONTROLLER DISCONNECTED");

    }

    public void OnFace_Up(InputAction.CallbackContext callbackContext)
    {
        face_up.Invoke(callbackContext);
    }

    public void OnFace_Down(InputAction.CallbackContext callbackContext)
    {
        face_down.Invoke(callbackContext);
    }

    public void OnFace_Left(InputAction.CallbackContext callbackContext)
    {
        face_left.Invoke(callbackContext);
    }

    public void OnFace_Right(InputAction.CallbackContext callbackContext)
    {
        face_right.Invoke(callbackContext);
    }
    
    public void OnShoulder_Left(InputAction.CallbackContext callbackContext)
    {
        shoulder_left.Invoke(callbackContext);
    }
    
    public void OnShoulder_Right(InputAction.CallbackContext callbackContext)
    {
        shoulder_right.Invoke(callbackContext);
    }

    public void OnTrigger_Left(InputAction.CallbackContext callbackContext)
    {
        trigger_left.Invoke(callbackContext);
    }

    public void OnTrigger_Right(InputAction.CallbackContext callbackContext)
    {
        trigger_right.Invoke(callbackContext);
    }

    public void OnDpad_Up(InputAction.CallbackContext callbackContext)
    {
        dpad_up.Invoke(callbackContext);
    }
    
    public void OnDpad_Down(InputAction.CallbackContext callbackContext)
    {
        dpad_down.Invoke(callbackContext);
    }
    
    public void OnDpad_Left(InputAction.CallbackContext callbackContext)
    {
        dpad_left.Invoke(callbackContext);
    }
    
    public void OnDpad_Right(InputAction.CallbackContext callbackContext)
    {
        dpad_right.Invoke(callbackContext);
    }
    
    public void OnLeftStick_X(InputAction.CallbackContext callbackContext)
    {
        leftStick_x.Invoke(callbackContext);
    }
    
    public void OnLeftStick_Y(InputAction.CallbackContext callbackContext)
    {
        leftStick_y.Invoke(callbackContext);
    }
    
    public void OnRightStick_X(InputAction.CallbackContext callbackContext)
    {
        rightStick_x.Invoke(callbackContext);
    }
    
    public void OnRightStick_Y(InputAction.CallbackContext callbackContext)
    {
        rightStick_y.Invoke(callbackContext);
    }
}
