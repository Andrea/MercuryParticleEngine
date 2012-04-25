namespace ProjectMercury.Design.Emitters
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing.Design;
    using ProjectMercury.Emitters;

    public class PolygonEmitterTypeConverter : EmitterTypeConverter
    {
        protected override void AddDescriptors(List<SmartMemberDescriptor> descriptors)
        {
            base.AddDescriptors(descriptors);

            var type = typeof(PolygonEmitter);

            descriptors.AddRange(new SmartMemberDescriptor[]
            {
                new SmartMemberDescriptor(type.GetField("Close"),
                    new CategoryAttribute("Polygon"),
                    new DescriptionAttribute("True if the poly should be closed.")),

                new SmartMemberDescriptor(type.GetProperty("Points"),
                    new CategoryAttribute("Polygon"),
                    new EditorAttribute(typeof(PolygonPointCollectionEditor), typeof(UITypeEditor)),
                    new DescriptionAttribute("The points which describe the poly.")),

                new SmartMemberDescriptor(type.GetProperty("Rotation"),
                    new CategoryAttribute("Polygon"),
                    new DescriptionAttribute("The rotation of the poly in radians.")),

                new SmartMemberDescriptor(type.GetProperty("Scale"),
                    new CategoryAttribute("Polygon"),
                    new DescriptionAttribute("The scale factor of the poly.")),

                new SmartMemberDescriptor(type.GetProperty("Origin"),
                    new CategoryAttribute("Polygon"),
                    new DescriptionAttribute("Describes the origin of the polygon."))
            });
        }
    }
}