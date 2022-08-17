using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputInterface : MonoBehaviour
{
    public UnityEvent face_up;
    public UnityEvent face_down;
    public UnityEvent face_left;
    public UnityEvent face_right;
    
    public UnityEvent shoulder_left;
    public UnityEvent shoulder_right;
    
    public UnityEvent<float> trigger_left;
    public UnityEvent<float> trigger_right;

    public UnityEvent dpad_up;
    public UnityEvent dpad_down;
    public UnityEvent dpad_left;
    public UnityEvent dpad_right;
    
    public UnityEvent<float> leftStick_x;
    public UnityEvent<float> leftStick_y;
    
    public UnityEvent<float> rightStick_x;
    public UnityEvent<float> rightStick_y;
    
    public void Face_Up(InputAction.CallbackContext callbackContext)
    {
        face_up.Invoke();
    }
    
    public void Face_Down(InputAction.CallbackContext callbackContext)
    {
        face_down.Invoke();
    }
    
    public void Face_Left(InputAction.CallbackContext callbackContext)
    {
        face_left.Invoke();
    }
    
    public void Face_Right(InputAction.CallbackContext callbackContext)
    {
        face_right.Invoke();
    }
    
    public void Shoulder_Left(InputAction.CallbackContext callbackContext)
    {
        shoulder_left.Invoke();
    }
    
    public void Shoulder_Right(InputAction.CallbackContext callbackContext)
    {
        shoulder_right.Invoke();
    }
    
    public void Trigger_Left(InputAction.CallbackContext callbackContext)
    {
        trigger_left.Invoke(callbackContext.ReadValue<float>());
    }
    
    public void Trigger_Right(InputAction.CallbackContext callbackContext)
    {
        trigger_right.Invoke(callbackContext.ReadValue<float>());
    }
    
    public void Dpad_Up(InputAction.CallbackContext callbackContext)
    {
        dpad_up.Invoke();
    }
    
    public void Dpad_Down(InputAction.CallbackContext callbackContext)
    {
        dpad_down.Invoke();
    }
    
    public void Dpad_Left(InputAction.CallbackContext callbackContext)
    {
        dpad_left.Invoke();
    }
    
    public void Dpad_Right(InputAction.CallbackContext callbackContext)
    {
        dpad_right.Invoke();
    }
    
    public void LeftStick_X(InputAction.CallbackContext callbackContext)
    {
        leftStick_x.Invoke(callbackContext.ReadValue<float>());
    }
    
    public void LeftStick_Y(InputAction.CallbackContext callbackContext)
    {
        leftStick_y.Invoke(callbackContext.ReadValue<float>());
    }
    
    public void RightStick_X(InputAction.CallbackContext callbackContext)
    {
        rightStick_x.Invoke(callbackContext.ReadValue<float>());
    }
    
    public void RightStick_Y(InputAction.CallbackContext callbackContext)
    {
        rightStick_y.Invoke(callbackContext.ReadValue<float>());
    }
}
