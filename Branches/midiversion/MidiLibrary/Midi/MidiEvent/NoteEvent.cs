using System.ComponentModel;
using Sanford.Multimedia.Midi;

namespace BindingLibrary
{
    [TypeDescriptionProvider(typeof(MidiEventTypeDescriptorProvider))]
    public class NoteEvent : MidiEventBase
    {
        private int velocity;
        private bool pressed;

        public NoteEvent(ChannelCommand command, int midiChannel, int data1)
            : base(command, midiChannel, data1, 0)
        {
        }

        
        public int Velocity 
        {
            get
            {
                return velocity;
            }
            private set
            {
                if (value != velocity)
                {
                    velocity = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("Velocity"));
                }
            }
        }   
        
        public bool Pressed
        {
            get
            {
                return pressed;
            }
            private set
            {
                if (value != pressed)
                {
                    pressed = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("Pressed"));
                }
            }
        }


        public override void SetData(ChannelMessage midiChannelMessage)
        {
            if (midiChannelMessage.Command == ChannelCommand.NoteOff || midiChannelMessage.Command == ChannelCommand.NoteOn)
            {
                if (midiChannelMessage.MidiChannel != midiChannel) return;
                if (midiChannelMessage.Data1 != data1) return;

                
                Velocity = (midiChannelMessage.Command == ChannelCommand.NoteOff ) ? 0 : midiChannelMessage.Data2;
                Pressed = (midiChannelMessage.Command == ChannelCommand.NoteOff) ? false :  midiChannelMessage.Data2 > 0;
            }
        }

    }
}