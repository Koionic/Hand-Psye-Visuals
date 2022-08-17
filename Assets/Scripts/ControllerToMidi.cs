using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllerToMidi : MonoBehaviour
{
    private InputInterface _inputInterface;

    private MidiCommander _midiCommander;
    
    // Start is called before the first frame update
    void Awake()
    {
        _inputInterface = FindObjectOfType<InputInterface>();
        _midiCommander = FindObjectOfType<MidiCommander>();
    }

    private void Start()
    {
        _inputInterface.face_down.AddListener(FaceDown);
    }

    public void FaceDown(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.performed)
        {
            SendButtonMidi("FaceDownOn");
        }
        else if (callbackContext.canceled)
        {
            SendButtonMidi("FaceDownOff");
        }
    }

    void SendCCMidi(string midiName, float value)
    {
        _midiCommander.SendSignal(midiName, 0, value);
    }
    
    void SendButtonMidi(string midiName)
    {
        _midiCommander.SendSignal(midiName, 0, 100f);
    }
    
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
