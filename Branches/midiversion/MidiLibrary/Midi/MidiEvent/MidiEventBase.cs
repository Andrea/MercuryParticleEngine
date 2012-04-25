using System;
using System.ComponentModel;
using Sanford.Multimedia.Midi;

namespace BindingLibrary
{
    public abstract class MidiEventBase : INotifyPropertyChanged, IBindableObject
    {
        protected int data1;
        protected int data2;
        private readonly string bindingId;
        protected ChannelCommand channelCommand;
        protected int midiChannel;


        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null) PropertyChanged(this, args);
        }

        public event PropertyChangedEventHandler PropertyChanged;


/*
        public MidiInputEvent(ChannelMessage message)
        {
            //note-on command with velocity of 0 is note-off command
            if (message.Command == ChannelCommand.NoteOn && message.Data2 == 0)
            {
                Command = ChannelCommand.NoteOff;
            }
            else
            {
                Command = message.Command;
            }

            Command = message.Command;
            MidiChannel = message.MidiChannel;
            Data1 = message.Data1;

            bindableObject = new BindableObject(String.Format("MIDI_{0}_{1}_{2}", Command, MidiChannel, Data1),  this);
        }
*/

        public MidiEventBase(ChannelCommand channelCommand, int midiChannel, int data1, int data2)
        {
            this.channelCommand = channelCommand;
            this.midiChannel = midiChannel;
            this.data1 = data1;
            this.data2 = data2;

            bindingId = GetID(channelCommand, midiChannel, data1, data2);
        }


       /* public override bool Equals(Object obj)
        {
            var midiInputEvent = obj as IMidiEventListener;
            if (midiInputEvent == null)
            {
                return false;
            }

            return  Command == midiInputEvent.Command &&
                    MidiChannel == midiInputEvent.MidiChannel &&
                    Data1 == midiInputEvent.Data1;
        }
*/
        public abstract void SetData(ChannelMessage midiChannelMessage);


        
        public string BindingId
        {
            get { return bindingId; }
        }

        public object BindingObject
        {
            get { return this; }
        }


        public override string ToString()
        {
            string[] notes = { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B"  };
            string result;

            switch (channelCommand)
            {
                case ChannelCommand.NoteOn:
                    result = "Note On ";
                    result += notes[data1 % 12];
                    result += (data1 / 12) - 4;
                    break;
                case ChannelCommand.NoteOff:
                    result = "Note Off ";
                    result += notes[data1 % 12];
                    result += (data1 / 12) - 4;
                    break;
                case ChannelCommand.Controller:
                    result = "CC ";
                    result += data1;
                    break;
                case ChannelCommand.PitchWheel:
                    result = "Pitch Wheel";
                    break;
                case ChannelCommand.ProgramChange:
                    result = "Program Change ";
                    result += data1;
                    break;
                default:
                    result = channelCommand.ToString();
                    break;
            }
            return result;
        }

        public static string GetID(ChannelCommand command, int midiChannel, int data1, int data2)
        {

            string format;
            switch (command)
            {

                // ignore noteOff and make it noteOn.
                case (ChannelCommand.NoteOff):
                    format = String.Format("MIDI_{0}_{1}_{2}", ChannelCommand.NoteOn, midiChannel, data1);
                    break;

                default:
                    format = String.Format("MIDI_{0}_{1}_{2}", command, midiChannel, data1);
                    break;
            }

            return format;
        }

        public static string GetID(ChannelMessage message)
        {
            if (message != null)
            {
                return GetID(message.Command, message.MidiChannel, message.Data1, message.Data2);
            }
            return null;
        }
     
    }


}