namespace ProjectMercury.Design.Emitters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ProjectMercury.Emitters;

    public class ConeEmitterTypeConverter : EmitterTypeConverter
    {
        /// <summary>
        /// Adds the descriptors.
        /// </summary>
        /// <param name="descriptors">The descriptors.</param>
        protected override void AddDescriptors(List<SmartMemberDescriptor> descriptors)
        {
            base.AddDescriptors(descriptors);

            var type = typeof(ConeEmitter);

            descriptors.AddRange(new SmartMemberDescriptor[]
            {
                new SmartMemberDescriptor(type.GetField("Direction"),
                    new DescriptionAttribute("The angle (in radians) that the SpotEmitters beam is facing.")),

                new SmartMemberDescriptor(type.GetField("ConeAngle"),
                    new DescriptionAttribute("The angle (in radians) from edge to edge of the SpotEmitters beam."))
            });
        }
    }
}