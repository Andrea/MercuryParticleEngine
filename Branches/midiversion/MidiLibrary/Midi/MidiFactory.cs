
using System;
using Sanford.Multimedia.Midi;

namespace BindingLibrary
{
    public class MidiFactory : IBindableObjectFactory
    {

        private static MidiFactory instance;

        protected MidiFactory()
        {
        }

        public static MidiFactory Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MidiFactory();
                }

                return instance;
            }
        }



        public IBindableObject CreateObject(string description)
        {
            return CreateMidiEvent(description);
        }

        public MidiEventBase CreateMidiEvent(ChannelCommand command, int midiChannel, int data1, int data2)
        {
            return CreateMidiEvent(MidiEventBase.GetID(command, midiChannel, data1, data2));
        }

        public MidiEventBase CreateMidiEvent(string description)
        {
            try
            {

                if (string.IsNullOrEmpty(description)) return null;

                String[] elements = description.Split('_');

                // elements contains a list of 4 strings
                if (elements.Length < 4) return null;

                // if no midimessage
                if (!elements[0].ToLower().StartsWith("midi")) return null;



                MidiEventBase eventBase = MidiProcessor.Instance.GetMidiInputEvent(description);

                if (eventBase == null)
                {
                    var command = (ChannelCommand) Enum.Parse(typeof (ChannelCommand), elements[1]);

                    int midiChannel;
                    int data1;
                    switch (command)
                    {
                       //CC
                        case (ChannelCommand.Controller):
                        case (ChannelCommand.PitchWheel):

                            midiChannel = int.Parse(elements[2]);
                            data1 = int.Parse(elements[3]);
                            
                            eventBase = new ControlChangeEvent(command, midiChannel, data1);

                            break;

                        case (ChannelCommand.NoteOn):
                        case (ChannelCommand.NoteOff):

                            midiChannel = int.Parse(elements[2]);
                            data1 = int.Parse(elements[3]);
                            
                            eventBase = new NoteEvent(command, midiChannel, data1);

                            break;
                    }
                    
                    // register the eventListener
                    if (eventBase != null) MidiProcessor.Instance.RegisterMidiInputEvent(eventBase);

                }

                return eventBase;

            }
            catch (Exception e)
            {
                Console.Out.WriteLine(e.ToString());
            }
            
            return null;
            
        }



    }
}