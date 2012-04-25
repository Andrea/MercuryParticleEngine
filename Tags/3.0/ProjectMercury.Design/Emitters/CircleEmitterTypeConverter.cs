namespace ProjectMercury.Design.Emitters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ProjectMercury.Emitters;

    public class CircleEmitterTypeConverter : EmitterTypeConverter
    {
        /// <summary>
        /// Adds the descriptors.
        /// </summary>
        /// <param name="descriptors">The descriptors.</param>
        protected override void AddDescriptors(List<SmartMemberDescriptor> descriptors)
        {
            base.AddDescriptors(descriptors);

            var type = typeof(CircleEmitter);

            descriptors.AddRange(new SmartMemberDescriptor[]
            {
#if DEBUG
                new SmartMemberDescriptor(type.GetProperty("Radius"),
                    new CategoryAttribute("Circle Emitter"),
                    new DescriptionAttribute("The radius of the circle in pixels.")),
#else
                new SmartMemberDescriptor(type.GetField("Radius"),
                    new CategoryAttribute("Circle Emitter"),
                    new DescriptionAttribute("The radius of the circle in pixels.")),
#endif
                new SmartMemberDescriptor(type.GetField("Ring"),
                    new CategoryAttribute("Circle Emitter"),
                    new DescriptionAttribute("True if the Emitter should release Particles only from the edge of the circle.")),

                new SmartMemberDescriptor(type.GetField("Radiate"),
                    new CategoryAttribute("Circle Emitter"),
                    new DescriptionAttribute("True if the Particles should radiate away from the center of the Emitter.")),
            });
        }
    }
}