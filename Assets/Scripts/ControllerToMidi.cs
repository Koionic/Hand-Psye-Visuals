using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerToMidi : MonoBehaviour
{
    private InputInterface _inputInterface;

    private MidiCommander _midiCommander;

    private float targetLeftX;
    private float targetLeftY;
    private float targetRightX;
    private float targetRightY;
    
    private float currentLeftX;
    private float currentLeftY;
    private float currentRightX;
    private float currentRightY;

    private float leftXSpeed;
    private float leftYSpeed;
    private float rightXSpeed;
    private float rightYSpeed;
    
    public float lerpSmoothness = 0.2f;
    public float lerpMaxSpeed = 0.2f;
    
    // Start is called before the first frame update
    void Awake()
    {
        _inputInterface = FindObjectOfType<InputInterface>();
        _midiCommander = FindObjectOfType<MidiCommander>();
    }
    
    

    private void Start()
    {
        _inputInterface.face_up.AddListener(FaceUp);
        _inputInterface.face_down.AddListener(FaceDown);
        _inputInterface.face_left.AddListener(FaceLeft);
        _inputInterface.face_right.AddListener(FaceRight);
        
        _inputInterface.dpad_up.AddListener(DpadUp);
        _inputInterface.dpad_down.AddListener(DpadDown);
        _inputInterface.dpad_left.AddListener(DpadLeft);
        _inputInterface.dpad_right.AddListener(DpadRight);
        
        _inputInterface.trigger_left.AddListener(LeftTrigger);
        _inputInterface.trigger_right.AddListener(RightTrigger);
        
        _inputInterface.shoulder_left.AddListener(LeftShoulder);
        _inputInterface.shoulder_right.AddListener(RightShoulder);
        
        _inputInterface.leftStick_x.AddListener(LeftStick_X);
        _inputInterface.leftStick_y.AddListener(LeftStick_Y);
        
        _inputInterface.rightStick_x.AddListener(RightStick_X);
        _inputInterface.rightStick_y.AddListener(RightStick_Y);
    }

    private void FixedUpdate()
    {
        LerpValueThenSendMidi(ref currentLeftX, ref targetLeftX, ref leftXSpeed, ControllerInputButton.Analogue_LeftX);
        LerpValueThenSendMidi(ref currentLeftY, ref targetLeftY, ref leftYSpeed, ControllerInputButton.Analogue_LeftY);
        LerpValueThenSendMidi(ref currentRightX, ref targetRightX, ref rightXSpeed, ControllerInputButton.Analogue_RightX);
        LerpValueThenSendMidi(ref currentRightY, ref targetRightY, ref rightYSpeed, ControllerInputButton.Analogue_RightY);
    }

    public void LerpValueThenSendMidi(ref float currentValue, ref float targetValue, ref float refTargetValue, ControllerInputButton button)
    {
        bool updateMidi = false;
        
        if (Mathf.Abs(targetValue - currentValue) > 0.01f)
        {
            currentValue = Mathf.SmoothDamp(currentValue, targetValue, ref refTargetValue, lerpSmoothness, lerpMaxSpeed);

            updateMidi = true;
        }
        else if (currentValue != targetValue)
        {
            currentValue = targetValue;
            
            updateMidi = true;
        }

        if (updateMidi)
        {
            SendCCMidi(button, currentValue);
        }
    }
    
    void SendCCMidi(ControllerInputButton inputButton, float value)
    {
        _midiCommander.SendSignal(inputButton, 0, value);
    }
    
    void SendButtonMidi(ControllerInputButton inputButton, InputActionPhase actionPhase)
    {
        _midiCommander.SendSignal(inputButton, 0, 100f, actionPhase);
    }

    public void FaceUp(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Face_Up, callbackContext.phase);
    }
    
    public void FaceDown(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Face_Down, callbackContext.phase);
    }
    
    public void FaceLeft(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Face_Left, callbackContext.phase);
    }
    
    public void FaceRight(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Face_Right, callbackContext.phase);
    }

    public void DpadUp(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Dpad_Up, callbackContext.phase);
    }
    
    public void DpadDown(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Dpad_Down, callbackContext.phase);
    }
    
    public void DpadLeft(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Dpad_Left, callbackContext.phase);
    }
    
    public void DpadRight(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Dpad_Right, callbackContext.phase);
    }
    
    public void LeftStick_X(InputAction.CallbackContext callbackContext)
    {
        targetLeftX = (callbackContext.ReadValue<float>() + 1f) / 2f;
        //SendCCMidi(ControllerInputButton.Analogue_LeftX, callbackContext.phase, callbackContext.ReadValue<float>());
    }
    
    public void LeftStick_Y(InputAction.CallbackContext callbackContext)
    {
        targetLeftY = (callbackContext.ReadValue<float>() + 1f) / 2f;
        //SendCCMidi(ControllerInputButton.Analogue_LeftY, callbackContext.phase, callbackContext.ReadValue<float>());
    }
    
    public void RightStick_X(InputAction.CallbackContext callbackContext)
    {
        targetRightX = (callbackContext.ReadValue<float>() + 1f) / 2f;
        //SendCCMidi(ControllerInputButton.Analogue_RightX, callbackContext.phase, callbackContext.ReadValue<float>());
    }
    
    public void RightStick_Y(InputAction.CallbackContext callbackContext)
    {
        targetRightY = (callbackContext.ReadValue<float>() + 1f) / 2f;
        //SendCCMidi(ControllerInputButton.Analogue_RightY, callbackContext.phase, callbackContext.ReadValue<float>());
    }

    public void LeftShoulder(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Shoulder_Left, callbackContext.phase);
    }
    
    public void RightShoulder(InputAction.CallbackContext callbackContext)
    {
        SendButtonMidi(ControllerInputButton.Shoulder_Right, callbackContext.phase);
    }
    
    public void LeftTrigger(InputAction.CallbackContext callbackContext)
    {
        SendCCMidi(ControllerInputButton.Trigger_Left, callbackContext.ReadValue<float>());
    }
    
    public void RightTrigger(InputAction.CallbackContext callbackContext)
    {
        SendCCMidi(ControllerInputButton.Trigger_Right, callbackContext.ReadValue<float>());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
