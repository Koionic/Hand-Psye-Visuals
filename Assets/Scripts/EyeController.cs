using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EyeController : MonoBehaviour
{
    private InputInterface _inputInterface;
    
    public GameObject eyeObj;

    public bool leftEye;

    public float eyeRotXZLerp;
    
    private Vector2 currentEyeRotXZ = new Vector2();
    private Vector2 targetEyeRotXZ = new Vector2();

    public Vector2 eyeRotAdjustmentXZ = new Vector2();
    public Vector2 eyeRotScaleXZ = new Vector2();

    
    
    private void Awake()
    {
        _inputInterface = FindObjectOfType<InputInterface>();
    }

    // Start is called before the first frame update
    void Start()
    {
        if (leftEye)
        {
            _inputInterface.leftStick_x.AddListener(OnStickX);
            _inputInterface.leftStick_y.AddListener(OnStickY);
        }
        else
        {
            _inputInterface.rightStick_x.AddListener(OnStickX);
            _inputInterface.rightStick_y.AddListener(OnStickY);
        }
    }

    // Update is called once per frame
    void Update()
    {
        LerpRot();
        
        SetEyeRot();
    }

    void LerpRot()
    {
        LerpValue(ref currentEyeRotXZ.x, ref targetEyeRotXZ.x, eyeRotXZLerp);
        LerpValue(ref currentEyeRotXZ.y, ref targetEyeRotXZ.y, eyeRotXZLerp);
    }

    void LerpValue(ref float current, ref float target, float lerp)
    {
        if (Mathf.Abs(target - current) <= 0.05f)
        {
            current = target;
            return;
        }

        current = Mathf.Lerp(current, target, lerp);
    }
    
    void OnStickX(InputAction.CallbackContext callbackContext)
    {
        targetEyeRotXZ.y = callbackContext.ReadValue<float>();
    }
    
    void OnStickY(InputAction.CallbackContext callbackContext)
    {
        targetEyeRotXZ.x = callbackContext.ReadValue<float>();
    }

    void SetEyeRot()
    {
        if (eyeObj)
        {
            Vector3 eyeRot = new Vector3();

            eyeRot.x = (currentEyeRotXZ.x * eyeRotScaleXZ.x) + eyeRotAdjustmentXZ.x;
            eyeRot.y = (currentEyeRotXZ.y * eyeRotScaleXZ.y) + eyeRotAdjustmentXZ.y;

            eyeObj.transform.eulerAngles = eyeRot;
        }
    }
}
