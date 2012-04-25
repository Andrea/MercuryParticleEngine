using System;
using System.ComponentModel;

namespace BindingLibrary
{
    public class ControlChangeEventTypeDescriptor : CustomTypeDescriptor<ControlChangeEvent>
    {
        private readonly Type ModifierType = typeof(ControlChangeEvent);


        public ControlChangeEventTypeDescriptor()
        {

            PropertyDescriptionCollection.Add(new CustomPropertyDescriptor(
                                                  ModifierType.GetProperty("Value"),
                                                  new CategoryAttribute("Midi"),
                                                  new DisplayNameAttribute("Value"),
                                                  new DescriptionAttribute("The Control Change Value")
                                                  ));
        }

        public override PropertyDescriptor GetDefaultProperty()
        {
            return PropertyDescriptionCollection[0];
        }

    }
}
