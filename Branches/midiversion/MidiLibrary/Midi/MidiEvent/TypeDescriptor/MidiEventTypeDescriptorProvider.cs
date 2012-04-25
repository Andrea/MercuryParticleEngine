namespace BindingLibrary
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Defines a factory class for getting controller type descriptors.
    /// </summary>
    public sealed class MidiEventTypeDescriptorProvider : TypeDescriptionProvider
    {



        /// <summary>
        /// Gets a custom type descriptor for the given type and object.
        /// </summary>
        /// <param name="objectType">The type of object for which to retrieve the type descriptor.</param>
        /// <param name="instance">An instance of the type. Can be null if no instance was passed to the <see cref="T:System.ComponentModel.TypeDescriptor"/>.</param>
        /// <returns>
        /// An <see cref="T:System.ComponentModel.ICustomTypeDescriptor"/> that can provide metadata for the type.
        /// </returns>
        public override ICustomTypeDescriptor GetTypeDescriptor(Type objectType, Object instance)
        {
            if (objectType == typeof(ControlChangeEvent))
                return new ControlChangeEventTypeDescriptor();


            if (objectType == typeof(NoteEvent))
                return new NoteEventEventTypeDescriptor();


            return base.GetTypeDescriptor(objectType, instance);
        }
    }


}