using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using RtMidi.LowLevel;
using UnityEngine;
using UnityEngine.InputSystem;

public class MidiOutPortEx
    {
        public string name;
        public MidiOutPort midiOutPort;

        public void Dispose()
        {
            midiOutPort?.Dispose();
        }
    }
    
    public class SingletonMidiDeviceManager : SingletonMonoBehaviour<SingletonMidiDeviceManager>
    {
        #region public

        #endregion

        #region Private members
        private MidiProbe _probe;

        private MidiOutPortEx _port = new MidiOutPortEx();
        private uint _portCount = 0;

        // Does the port seem real or not?
        // This is mainly used on Linux (ALSA) to filter automatically generated
        // virtual ports.
        bool IsRealPort(string name)
        {
            return !name.Contains("Through") && !name.Contains("RtMidi");
        }

        MidiOutPortEx ScanPortEx(string deviceName)
        {
            if(_probe == null) Init();
            MidiOutPortEx portEx = new MidiOutPortEx();
            _portCount = 0;
            for (var i = 0; i < _probe.PortCount; i++)
            {
                var name = _probe.GetPortName(i);
                if (name.Contains(deviceName))
                {
                    portEx.midiOutPort = IsRealPort(name) ? new MidiOutPort(i) : null;
                    portEx.name = name;
                    Debug.Log(name);
                }

                _portCount++;
            }

            return portEx;
        }

        #endregion

        #region MonoBehaviour implementation


        void Start()
        {
           Init();
        }

        void Init()
        {
            _probe = new MidiProbe(MidiProbe.Mode.Out);
        }
  
        void OnDestroy()
        {
            _probe?.Dispose();
        }

        #endregion

        public MidiOutPortEx GetPortEx(string deviceName)
        {
            return ScanPortEx(deviceName);
        }

       
    }