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
}
