using System;
using System.Collections.Generic;
using Sanford.Multimedia.Midi;

namespace BindingLibrary
{
    public class MidiProcessor
    {
        private static MidiProcessor instance;

        private readonly Dictionary<string, MidiEventBase> midiInputEventTable;
        private bool enabled;

        protected MidiProcessor()
        {
            midiInputEventTable = new Dictionary<string, MidiEventBase>();

            Enabled = true;
        }


        public static MidiProcessor Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MidiProcessor();
                }

                return instance;
            }
        }

        public bool Enabled
        {
            get
            {
                return enabled;
            }
            set
            {
                if (enabled != value)
                {
                    enabled = value;

                    if (enabled)
                    {
                        MidiDeviceManager.Instance.ChannelMessageReceived += MidiDeviceManager_ChannelMessageReceived;    
                    }
                    else
                    {
                        MidiDeviceManager.Instance.ChannelMessageReceived -= MidiDeviceManager_ChannelMessageReceived;    
                    }

                }
            }
        }


        public MidiEventBase GetMidiInputEvent(string id)
        {
            if (String.IsNullOrEmpty(id)) return null;

            MidiEventBase midiEventBase;
            
            return midiInputEventTable.TryGetValue(id, out midiEventBase) ? midiEventBase : null;
        }

        public void RegisterMidiInputEvent(MidiEventBase eventBase)
        {
            if (eventBase == null) return;

            if (GetMidiInputEvent(eventBase.BindingId) == null) midiInputEventTable.Add(eventBase.BindingId, eventBase);
        }

        public void UnRegisterMidiInputEvent(MidiEventBase eventBase)
        {
            if (eventBase == null) return;

            if (GetMidiInputEvent(eventBase.BindingId) != null) midiInputEventTable.Remove(eventBase.BindingId);
        }

        private void MidiDeviceManager_ChannelMessageReceived(object sender, ChannelMessageEventArgs e)
        {
            MidiEventBase eventBase = GetMidiInputEvent(MidiEventBase.GetID(e.Message));

            if (eventBase != null) eventBase.SetData(e.Message);
        }



    }
}
