using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class MidiLoopOut : MonoBehaviour
{
    [SerializeField] private string deviceName;
    [SerializeField] private int channel = 0;
    private MidiOutPortEx _port;

    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        _port = SingletonMidiDeviceManager.Instance.GetPortEx(deviceName);
        _playerInput = GetComponent<PlayerInput>();
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        _port?.Dispose();
    }
    
    public void OnActionLoopBack(InputAction.CallbackContext context)
    {
       
        var noteName = context.control.name;
        int note = int.Parse(Regex.Replace(noteName, @"[^0-9]", ""));
        var value = context.ReadValue<float>();
        _port?.midiOutPort?.SendNoteOn(channel,note, (int)value);
    }


    public void DebugLogAndLoopBack(InputAction.CallbackContext context)
    {
        var noteName = context.control.name;
        int note = int.Parse(Regex.Replace(noteName, @"[^0-9]", ""));
        var value = context.ReadValue<float>();
        _port?.midiOutPort?.SendNoteOn(channel, note, (int)value);
        Debug.Log("device: " + context.control.device.displayName + " channel: " + channel + " note: " + note + " value: " + value);
    }
}
