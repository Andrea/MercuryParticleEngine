namespace ProjectMercury.Design.Emitters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ProjectMercury.Emitters;

    public class LineEmitterTypeConverter : EmitterTypeConverter
    {
        /// <summary>
        /// Adds the descriptors.
        /// </summary>
        /// <param name="descriptors">The descriptors.</param>
        protected override void AddDescriptors(List<SmartMemberDescriptor> descriptors)
        {
            base.AddDescriptors(descriptors);

            var type = typeof(LineEmitter);

            descriptors.AddRange(new SmartMemberDescriptor[]
            {
#if DEBUG
                new SmartMemberDescriptor(type.GetProperty("Length"),
                    new CategoryAttribute("Line Emitter"),
                    new DescriptionAttribute("The length of the line in pixels.")),

                new SmartMemberDescriptor(type.GetProperty("Angle"),
                    new CategoryAttribute("Line Emitter"),
                    new DescriptionAttribute("The angle of the line in radians.")),
#else
                new SmartMemberDescriptor(type.GetField("Length"),
                    new CategoryAttribute("Line Emitter"),
                    new DescriptionAttribute("The length of the line in pixels.")),

                new SmartMemberDescriptor(type.GetField("Angle"),
                    new CategoryAttribute("Line Emitter"),
                    new DescriptionAttribute("The angle of the line in radians.")),
#endif
                new SmartMemberDescriptor(type.GetField("Rectilinear"),
                    new CategoryAttribute("Line Emitter"),
                    new DescriptionAttribute("True if the Emitter should release Particles perpendicular to the angle of the line.")),
            });
        }
    }
}