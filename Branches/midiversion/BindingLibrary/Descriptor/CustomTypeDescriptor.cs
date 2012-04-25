
namespace BindingLibrary
{
    using System;
    using System.ComponentModel;

    /// <summary>
    /// Defines the abstract base class for a type descriptor.
    /// </summary>
    public abstract class CustomTypeDescriptor<T> : CustomTypeDescriptor where T : class
    {
        private readonly Type Type = typeof(T);
        private readonly PropertyDescriptorCollection propertyDescriptionCollection;


        protected CustomTypeDescriptor()
        {
            propertyDescriptionCollection = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
        }

        /// <summary>
        /// Returns the fully qualified name of the class represented by this type descriptor.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing the fully qualified class name of the type this type descriptor is describing. The default is null.
        /// </returns>
        public override string GetClassName()
        {
            return Type.FullName;
        }

        /// <summary>
        /// Returns the name of the class represented by this type descriptor.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"/> containing the name of the component instance this type descriptor is describing. The default is null.
        /// </returns>
        public override string GetComponentName()
        {
            return Type.Name;
        }

        public PropertyDescriptorCollection PropertyDescriptionCollection
        {
            get { return propertyDescriptionCollection; }
        }


        public override PropertyDescriptorCollection GetProperties()
        {
            return ((ICustomTypeDescriptor)this).GetProperties(null);
        }


        public override PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return propertyDescriptionCollection;
        }

        public override PropertyDescriptor GetDefaultProperty()
        {
            return base.GetDefaultProperty();
        }
    }
}