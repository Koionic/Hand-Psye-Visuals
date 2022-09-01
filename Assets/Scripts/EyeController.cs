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
    private InputInterface _inputInterface;
    
    public GameObject eyeObj;

    public bool leftEye;

    public float eyeRotXZLerp;
    
    private Vector2 currentEyeRotXZ = new Vector2();
    private Vector2 targetEyeRotXZ = new Vector2();

    public Vector2 eyeRotAdjustmentXZ = new Vector2();
    public Vector2 eyeRotScaleXZ = new Vector2();

    public Coroutine autoPilotTimer = null;
    
    public float autoPilotDelay;
    public bool autoPilotOn;

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
        

        int random = _random.Next(0, 32);
        
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
        targetEyeRotXZ.x = (Mathf.PerlinNoise(autoNoiseSeed, Time.time * 1.2f) * 2f) - 1f;
        //targetEyeRotXZ.x = Mathf.Pow(targetEyeRotXZ.x, 3f);

        targetEyeRotXZ.y = (Mathf.PerlinNoise(Time.time * 1.1f, autoNoiseSeed) * 2f) - 1f;
        //targetEyeRotXZ.y = Mathf.Pow(targetEyeRotXZ.y, 3f);
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

        autoPilotOn = false;
    }

    IEnumerator AutoPilotTimer()
    {
        yield return new WaitForSecondsRealtime(autoPilotDelay);

        autoPilotOn = true;
    }


}
