namespace ProjectMercury.Design.Modifiers
{
    using System.ComponentModel;
    using System.Drawing.Design;
    using ProjectMercury.Design.UITypeEditors;
    using ProjectMercury.Modifiers;

    public class RandomColourModifierTypeConverter : ExpandableObjectConverter
    {
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, System.Attribute[] attributes)
        {
            var type = typeof(RandomColourModifier);

            return new PropertyDescriptorCollection(new PropertyDescriptor[]
            {
                new SmartMemberDescriptor(type.GetField("Colour"),
                    new EditorAttribute(typeof(ColourEditor), typeof(UITypeEditor)),
                    new DescriptionAttribute("The base colour of Particles as they are released.")),

                new SmartMemberDescriptor(type.GetField("Variation"),
                    new DescriptionAttribute("The random variation factor of the base colour."))
            });
        }
    }
}