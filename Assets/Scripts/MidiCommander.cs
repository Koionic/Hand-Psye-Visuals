using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public enum MidiSignalType
{
    NoteOn,
    NoteOff,
    ButtonNote,
    ControlChange
}

public enum ControllerInputButton
{
    Analogue_LeftX,
    Analogue_LeftY,
    Analogue_LeftClick,
    Analogue_RightX,
    Analogue_RightY,
    Analogue_RightClick,
    Shoulder_Left,
    Shoulder_Right,
    Trigger_Left,
    Trigger_Right,
    Dpad_Up,
    Dpad_Down,
    Dpad_Left,
    Dpad_Right,
    Face_Up,
    Face_Down,
    Face_Left,
    Face_Right,
    Select,
    Start
}

[Serializable]
public class MidiCommand
{
    public string name;
    public ControllerInputButton inputButton;
    public MidiSignalType signalType;
    public MidiChannel channel;

    /// <summary>
    /// Sends out a midi signal based on the parameters set in the MidiCommand class
    /// </summary>
    /// <param name="addressNumber">
    /// Note Number when NoteOn/NoteOff, Controller Number when Control Change
    /// </param>
    /// <param name="changeValue">
    /// Velocity when NoteOn/NoteOff, Midi Value when Control Change
    /// </param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public void ExecuteSignal(InputActionPhase actionPhase, int addressNumber, float changeValue)
    {
        switch (signalType)
        {
            case MidiSignalType.ButtonNote:
                switch (actionPhase)
                {
                    case InputActionPhase.Performed:
                        MidiOut.SendNoteOn(channel, addressNumber, changeValue);
                        Debug.Log(name + " SENDING NOTE ON THROUGH CHANNEL " + channel + " - NOTE " + addressNumber + " - VELOCITY " + changeValue);
                        break;
                    case InputActionPhase.Canceled:
                        MidiOut.SendNoteOff(channel, addressNumber);
                        Debug.Log(name + " SENDING NOTE OFF THROUGH CHANNEL " + channel + " - NOTE " + addressNumber);
                        break;
                }
                break;
            case MidiSignalType.NoteOn:
                MidiOut.SendNoteOn(channel, addressNumber, changeValue);
                Debug.Log(name + " SENDING NOTE ON THROUGH CHANNEL " + channel + " - NOTE " + addressNumber + " - VELOCITY " + changeValue);
                break;
            case MidiSignalType.NoteOff:
                MidiOut.SendNoteOff(channel, addressNumber);
                Debug.Log(name + " SENDING NOTE OFF THROUGH CHANNEL " + channel + " - NOTE " + addressNumber);
                break;
            case MidiSignalType.ControlChange:
                MidiOut.SendControlChange(channel, addressNumber, changeValue);
                Debug.Log(name + " SENDING CONTROL CHANGE THROUGH CHANNEL " + channel + " - CONTROLLER " + addressNumber + " - MIDI VALUE " + changeValue);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}

public class MidiCommander : MonoBehaviour
{
    public List<MidiCommand> _midiCommands = new List<MidiCommand>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SendSignal(ControllerInputButton buttonToSearch, int addressNumber, float changeValue, InputActionPhase actionPhase = InputActionPhase.Canceled)
    {
        for (int i = 0; i < _midiCommands.Count; i++)
        {
            if (_midiCommands[i].inputButton == buttonToSearch)
            {
                _midiCommands[i].ExecuteSignal(actionPhase, addressNumber, changeValue);
            }
        }
    }
}
