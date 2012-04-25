namespace ProjectMercury.Design.Modifiers
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Design;
    using ProjectMercury.Design.UITypeEditors;
    using ProjectMercury.Modifiers;

    public class ColorModifierTypeConverter : ExpandableObjectConverter
    {
        /// <summary>
        /// Gets a collection of properties for the type of object specified by the value parameter.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext"/> that provides a format context.</param>
        /// <param name="value">An <see cref="T:System.Object"/> that specifies the type of object to get the properties for.</param>
        /// <param name="attributes">An array of type <see cref="T:System.Attribute"/> that will be used as a filter.</param>
        /// <returns>
        /// A <see cref="T:System.ComponentModel.PropertyDescriptorCollection"/> with the properties that are exposed for the component, or null if there are no properties.
        /// </returns>
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            var type = typeof(ColorModifier);

            return new PropertyDescriptorCollection(new PropertyDescriptor[]
            {
                new SmartMemberDescriptor(type.GetField("InitialColour"),
                    new EditorAttribute(typeof(ColourEditor), typeof(UITypeEditor)),
                    new DisplayNameAttribute("Initial Colour"),
                    new DescriptionAttribute("The initial colour of Particles when they are released from the Emitter.")),

                new SmartMemberDescriptor(type.GetField("UltimateColour"),
                    new EditorAttribute(typeof(ColourEditor), typeof(UITypeEditor)),
                    new DisplayNameAttribute("Ultimate Colour"),
                    new DescriptionAttribute("The ultimate colour of Particles at the point of being retired."))
            });
        }
    }
}