using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MFunctions;

public enum EyeDirection
{
    Up,
    Down,
    Left,
    Right
}

public class HeadController : MonoBehaviour
{
    public Coroutine turningEyeball;

    private EyeDirection _eyeDirection;

    public float eyeballTurnLerp;

    public EyeController leftEye;
    public EyeController rightEye;

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
        
    }
    
    void OnDpadUp(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            TriggerEyeballTurn(EyeDirection.Up);
        }
    }
    
    void OnDpadDown(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            TriggerEyeballTurn(EyeDirection.Down);
        }
    }
    
    void OnDpadLeft(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            TriggerEyeballTurn(EyeDirection.Left);
        }
    }
    
    void OnDpadRight(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            TriggerEyeballTurn(EyeDirection.Right);
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
            case EyeDirection.Up:
                targetRot = 360f;
                break;
            case EyeDirection.Down:
                targetRot = -360f;
                break;
            case EyeDirection.Left:
                targetRot = 360f;
                break;
            case EyeDirection.Right:
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
                    switch (_eyeDirection)
                    {
                        case EyeDirection.Up:
                            
                            break;
                        case EyeDirection.Down:
                            break;
                        case EyeDirection.Left:
                            break;
                        case EyeDirection.Right:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    
                    sentDpadSignalToResolume = true;
                }
                
                MathFunctions.LerpValue(ref currentRot, targetRot, eyeballTurnLerp * 3f);
            }

            switch (_eyeDirection)
            {
                case EyeDirection.Up:
                case EyeDirection.Down:
                    leftEye.eyeRotAdjustmentXZ.x = currentRot;
                    rightEye.eyeRotAdjustmentXZ.x = currentRot;
                    break;
                case EyeDirection.Left:
                case EyeDirection.Right:
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
    
    void TriggerEyeballTurn(EyeDirection eyeDir)
    {
        if (turningEyeball != null)
        {
            return;
        }
        
        _eyeDirection = eyeDir;
        
        turningEyeball = StartCoroutine(TurningEyeball());
    }
}
