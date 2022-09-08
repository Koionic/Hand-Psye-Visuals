using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MFunctions;


public class EyeInfo
{
    public bool turningEyeball;
    public Vector2 eyeRotAdjustmentXZ;

    public EyeInfo(bool turningEyeball = false, Vector2 eyeRotAdjustmentXZ = new Vector2())
    {
        this.turningEyeball = turningEyeball;
        this.eyeRotAdjustmentXZ = eyeRotAdjustmentXZ;
    }
}

public class HeadController : MonoBehaviour
{
    public ControllerToMidi ControllerToMidi;
    
    public Coroutine turningEyeball;

    private ControllerInputButton _eyeDirection;

    public float eyeballTurnLerp;

    public EyeInfo leftEye = new EyeInfo();
    public EyeInfo rightEye = new EyeInfo();
    
    public List<EyeController> leftEyes;
    public List<EyeController> rightEyes;

    public InputInterface _inputInterface;

    public bool sentDpadSignalToResolume;
    
    // Start is called before the first frame update
    void Start()
    {
        _inputInterface.dpad_up.AddListener(OnDpadUp);
        _inputInterface.dpad_down.AddListener(OnDpadDown);
        _inputInterface.dpad_left.AddListener(OnDpadLeft);
        _inputInterface.dpad_right.AddListener(OnDpadRight);

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < leftEyes.Count; i++)
        {
            leftEyes[i].turningEyeball = leftEye.turningEyeball;
            leftEyes[i].eyeRotAdjustmentXZ = leftEye.eyeRotAdjustmentXZ;
        }

        for (int i = 0; i < rightEyes.Count; i++)
        {
            rightEyes[i].turningEyeball = rightEye.turningEyeball;
            rightEyes[i].eyeRotAdjustmentXZ = rightEye.eyeRotAdjustmentXZ;
        }
    }
    
    void OnDpadUp(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            TriggerEyeballTurn(ControllerInputButton.Dpad_Up);
        }
    }
    
    void OnDpadDown(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            TriggerEyeballTurn(ControllerInputButton.Dpad_Down);
        }
    }
    
    void OnDpadLeft(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            TriggerEyeballTurn(ControllerInputButton.Dpad_Left);
        }
    }
    
    void OnDpadRight(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            TriggerEyeballTurn(ControllerInputButton.Dpad_Right);
        }
    }
    
    IEnumerator TurningEyeball()
    {
        sentDpadSignalToResolume = false;
        
        leftEye.turningEyeball = true;
        rightEye.turningEyeball = true;
        
        float currentRot = 0;

        float targetRot = 0;

        switch (_eyeDirection)
        {
            case ControllerInputButton.Dpad_Up:
                targetRot = 360f;
                break;
            case ControllerInputButton.Dpad_Down:
                targetRot = -360f;
                break;
            case ControllerInputButton.Dpad_Left:
                targetRot = 360f;
                break;
            case ControllerInputButton.Dpad_Right:
                targetRot = -360f;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
        while (Mathf.Abs(targetRot - currentRot) > 0.05f)
        {
            if (Mathf.Abs(targetRot - currentRot) > 10f)
            {
                MathFunctions.LerpValue(ref currentRot, targetRot, eyeballTurnLerp);
            }
            else
            {
                if (!sentDpadSignalToResolume)
                {
                    if (ControllerToMidi)
                    {
                        ControllerToMidi.SendButtonMidi(_eyeDirection, InputActionPhase.Performed);
                    }

                    sentDpadSignalToResolume = true;
                }
                
                MathFunctions.LerpValue(ref currentRot, targetRot, eyeballTurnLerp * 3f);
            }

            switch (_eyeDirection)
            {
                case ControllerInputButton.Dpad_Up:
                case ControllerInputButton.Dpad_Down:
                    leftEye.eyeRotAdjustmentXZ.x = currentRot;
                    rightEye.eyeRotAdjustmentXZ.x = currentRot;
                    break;
                case ControllerInputButton.Dpad_Left:
                case ControllerInputButton.Dpad_Right:
                    leftEye.eyeRotAdjustmentXZ.y = currentRot;
                    rightEye.eyeRotAdjustmentXZ.y = currentRot;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
            yield return new WaitForFixedUpdate();
        }

        leftEye.eyeRotAdjustmentXZ = Vector2.zero;
        rightEye.eyeRotAdjustmentXZ = Vector2.zero;

        leftEye.turningEyeball = false;
        rightEye.turningEyeball = false;
        
        turningEyeball = null;

        yield return null;
    }
    
    void TriggerEyeballTurn(ControllerInputButton eyeDir)
    {
        if (turningEyeball != null)
        {
            return;
        }
        
        print("TriggerTurn");
        
        _eyeDirection = eyeDir;
        
        turningEyeball = StartCoroutine(TurningEyeball());
    }
}
