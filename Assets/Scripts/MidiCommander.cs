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
    ControlChange
}

[Serializable]
public class MidiCommand
{
    public string name;
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
    public void ExecuteSignal(int addressNumber, float changeValue)
    {
        switch (signalType)
        {
            case MidiSignalType.NoteOn:
                MidiOut.SendNoteOn(channel, addressNumber, changeValue);
                Debug.Log(name + " SENDING NOTE ON - NOTE " + addressNumber + " - VELOCITY " + changeValue);
                break;
            case MidiSignalType.NoteOff:
                MidiOut.SendNoteOff(channel, addressNumber);
                Debug.Log(name + " SENDING NOTE OFF - NOTE " + addressNumber);
                break;
            case MidiSignalType.ControlChange:
                MidiOut.SendControlChange(channel, addressNumber, changeValue);
                Debug.Log(name + " SENDING CONTROL CHANGE - CONTROLLER " + addressNumber + " - MIDI VALUE " + changeValue);
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

    public void SendSignal(string nameToSearch, int addressNumber, float changeValue)
    {
        for (int i = 0; i < _midiCommands.Count; i++)
        {
            if (_midiCommands[i].name == nameToSearch)
            {
                _midiCommands[i].ExecuteSignal(addressNumber, changeValue);
            }
        }
    }
}
