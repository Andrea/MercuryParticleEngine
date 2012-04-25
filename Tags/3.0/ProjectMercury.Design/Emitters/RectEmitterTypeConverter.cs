namespace ProjectMercury.Design.Emitters
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using ProjectMercury.Emitters;

    public class RectEmitterTypeConverter : EmitterTypeConverter
    {
        /// <summary>
        /// Adds the descriptors.
        /// </summary>
        /// <param name="descriptors">The descriptors.</param>
        protected override void AddDescriptors(List<SmartMemberDescriptor> descriptors)
        {
            base.AddDescriptors(descriptors);

            var type = typeof(RectEmitter);

            descriptors.AddRange(new SmartMemberDescriptor[]
            {
#if DEBUG
                new SmartMemberDescriptor(type.GetProperty("Width"),
                    new CategoryAttribute("Rect Emitter"),
                    new DescriptionAttribute("The width of the rectangle in pixels.")),

                new SmartMemberDescriptor(type.GetProperty("Height"),
                    new CategoryAttribute("Rect Emitter"),
                    new DescriptionAttribute("The height of the rectangle in pixels.")),
#else
                new SmartMemberDescriptor(type.GetField("Width"),
                    new CategoryAttribute("Rect Emitter"),
                    new DescriptionAttribute("The width of the rectangle in pixels.")),

                new SmartMemberDescriptor(type.GetField("Height"),
                    new CategoryAttribute("Rect Emitter"),
                    new DescriptionAttribute("The height of the rectangle in pixels.")),
#endif
                new SmartMemberDescriptor(type.GetField("Frame"),
                    new CategoryAttribute("Rect Emitter"),
                    new DescriptionAttribute("True if the Emitter should release Particles only from the edge of the rectangle.")),
            });
        }
    }
}