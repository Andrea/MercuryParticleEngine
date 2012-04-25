using System;
using System.ComponentModel;

namespace BindingLibrary
{
    public class NoteEventEventTypeDescriptor : CustomTypeDescriptor<NoteEvent>
    {
        private readonly Type ModifierType = typeof(NoteEvent);


        public NoteEventEventTypeDescriptor()
        {

            PropertyDescriptionCollection.Add(new CustomPropertyDescriptor(
                                                  ModifierType.GetProperty("Pressed"),
                                                  new CategoryAttribute("Midi"),
                                                  new DisplayNameAttribute("Pressed"),
                                                  new DescriptionAttribute("The Note Pressed status")
                                                  ));

            PropertyDescriptionCollection.Add(new CustomPropertyDescriptor(
                                                  ModifierType.GetProperty("Velocity"),
                                                  new CategoryAttribute("Midi"),
                                                  new DisplayNameAttribute("Velocity"),
                                                  new DescriptionAttribute("The Note Velocity")
                                                  ));
        }

        public override PropertyDescriptor GetDefaultProperty()
        {
            return PropertyDescriptionCollection[0];
        }

    }
}