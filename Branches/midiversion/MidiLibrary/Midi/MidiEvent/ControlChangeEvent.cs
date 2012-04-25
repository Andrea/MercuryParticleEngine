using System;
using System.ComponentModel;
using Sanford.Multimedia.Midi;

namespace BindingLibrary
{
    [TypeDescriptionProvider(typeof(MidiEventTypeDescriptorProvider))]
    public class ControlChangeEvent : MidiEventBase
    {
        public ControlChangeEvent(ChannelCommand command, int midiChannel, int data1): base(command, midiChannel, data1, 0)
        {
        }

        
        public int Value 
        {
            get
            {
                return data2;
            }
            private set
            {
                if (value != data2)
                {
                    data2 = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("Value"));
                }
            }
        }


       
        public override void SetData(ChannelMessage midiChannelMessage)
        {
            if (midiChannelMessage.Command != channelCommand ) return;
            if (midiChannelMessage.MidiChannel != midiChannel) return;
            if (midiChannelMessage.Data1 != data1) return;

            Value = midiChannelMessage.Data2;
        }

    }
}