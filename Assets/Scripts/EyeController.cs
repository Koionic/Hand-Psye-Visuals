using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = System.Random;
using MFunctions;



public class EyeController : MonoBehaviour
{
    [Header("References")]
    
    private InputInterface _inputInterface;
    
    public GameObject eyeObj;

    [Header("Stats")]
    
    public bool leftEye;

    public float eyeRotXZLerp;
    
    private Vector2 currentEyeRotXZ = new Vector2();
    private Vector2 targetEyeRotXZ = new Vector2();

    public Vector2 eyeRotAdjustmentXZ = new Vector2();
    public Vector2 eyeRotScaleXZ = new Vector2();

    [Header("Auto Pilot")]
    
    public Coroutine autoPilotTimer = null;
    
    public float autoPilotDelay;
    public bool autoPilotOn;

    public bool focusOn;

    public Vector2 focusPosition;
    
    public Coroutine focusIntervalTimer = null;

    public float autoFocusIntervalMin, autoFocusIntervalMax;
    
    public Coroutine focusDurationTimer = null;

    public float autoFocusDurationMin, autoFocusDurationMax;

    [Header("Overall Random Values")]
    
    private float autoNoiseSeed;

    private System.Random _random = new Random();

    public bool turningEyeball;

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
        

        int random = _random.Next(0, 128);
        
        Debug.Log("Random: " + random);
        
        autoNoiseSeed = random;
    }

    // Update is called once per frame
    void Update()
    {
        if (autoPilotOn)
        {
            AutoPilotLerp();
        }
        else if (autoPilotTimer == null && 
                 !turningEyeball &&
                 (targetEyeRotXZ.x == 0f || 
                 targetEyeRotXZ.y == 0f))
        {
            Debug.Log("STARTING COROUTINE");
            autoPilotTimer = StartCoroutine(AutoPilotTimer());
        }

        LerpRot();

        SetEyeRot();


    }

//new Vector2((Mathf.PerlinNoise(0f, Time.time) * 2) - 1, (Mathf.PerlinNoise(Time.time, 0f) * 2) - 1)

    void AutoPilotLerp()
    {
        if (focusOn)
        {
            targetEyeRotXZ.x = focusPosition.x;
            targetEyeRotXZ.y = focusPosition.y;
        }
        else
        {
            targetEyeRotXZ.x = (Mathf.PerlinNoise(autoNoiseSeed, Time.time * 1.2f) * 2f) - 1f;
            //targetEyeRotXZ.x = Mathf.Pow(targetEyeRotXZ.x, 3f);

            targetEyeRotXZ.y = (Mathf.PerlinNoise(Time.time * 1.1f, autoNoiseSeed) * 2f) - 1f;
            //targetEyeRotXZ.y = Mathf.Pow(targetEyeRotXZ.y, 3f);
        }
    }

    void LerpRot()
    {
        MathFunctions.LerpValue(ref currentEyeRotXZ.x, targetEyeRotXZ.x, eyeRotXZLerp);
        MathFunctions.LerpValue(ref currentEyeRotXZ.y, targetEyeRotXZ.y, eyeRotXZLerp);
    }


    
    void OnStickX(InputAction.CallbackContext callbackContext)
    {
        targetEyeRotXZ.y = callbackContext.ReadValue<float>();

        CancelAutoPilot();
    }

    void OnStickY(InputAction.CallbackContext callbackContext)
    {
        targetEyeRotXZ.x = callbackContext.ReadValue<float>();

        CancelAutoPilot();
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

    public void CancelAutoPilot()
    {
        if (autoPilotTimer != null)
        {
            StopCoroutine(autoPilotTimer);
            autoPilotTimer = null;
        }

        if (focusIntervalTimer != null)
        {
            StopCoroutine(focusIntervalTimer);
            focusIntervalTimer = null;
        }
        
        if (focusDurationTimer != null)
        {
            StopCoroutine(focusDurationTimer);
            focusDurationTimer = null;
        }

        autoPilotOn = false;
    }

    IEnumerator AutoPilotTimer()
    {
        yield return new WaitForSecondsRealtime(autoPilotDelay);

        autoPilotOn = true;

        StartCoroutine(FocusIntervalTimer());
    }
    
    IEnumerator FocusIntervalTimer()
    {
        float randTime = (float)_random.Next((int)(autoFocusIntervalMin * 100), (int)(autoFocusIntervalMax * 100)) / 100f;
        
        yield return new WaitForSecondsRealtime(randTime);

        float randX = (float)_random.Next(-100,100) / 100f;
        float randY = (float)_random.Next(-100,100) / 100f;

        focusPosition = new Vector2(randX, randY);
        
        focusOn = true;

        focusDurationTimer = StartCoroutine(FocusDurationTimer());

        focusIntervalTimer = null;
    }
    
    IEnumerator FocusDurationTimer()
    {
        float randTime = (float)_random.Next((int)(autoFocusDurationMin * 100), (int)(autoFocusDurationMax * 100)) / 100f;
        
        yield return new WaitForSecondsRealtime(randTime);

        focusPosition = Vector2.zero;
        
        focusOn = false;

        focusIntervalTimer = StartCoroutine(FocusIntervalTimer());

        focusDurationTimer = null;
    }
    
}
