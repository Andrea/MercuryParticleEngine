using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace BindingLibrary
{
    /// <summary>
    /// Represents a single property in a CustomPropertyDescriptor.
    /// </summary>
    public class CustomPropertyDescriptor : PropertyDescriptor
    {


        private TypeConverter conv;
        private readonly PropertyInfo propertyInfo;


        public CustomPropertyDescriptor(PropertyInfo propertyInfo, params Attribute[] attributes) 
            : base(propertyInfo.Name, attributes)
        {
            this.propertyInfo = propertyInfo;
        }



        public override TypeConverter Converter
        {
            get
            {
                if (conv == null)
                    conv = base.Converter.GetType() != typeof(TypeConverter) ? 
                                            base.Converter : 
                                            new ExpandableObjectConverter();

                return conv;

            }
        }


        public override bool IsReadOnly
        {
            get { return false; }
        }

       
        public override bool CanResetValue(object component)
        {
            return false;
        }


        public override void ResetValue(object component)
        {
        }

       /// <summary>
        /// When overridden in a derived class, gets the current value of the property on a component.
        /// </summary>
        /// <param name="component">The component with the property for which to retrieve the value.</param>
        /// <returns>
        /// The value of a property for a given component.
        /// </returns>
        public override Object GetValue(Object component)
        {
            return this.propertyInfo.GetValue(component, null);
        }

        /// <summary>
        /// When overridden in a derived class, sets the value of the component to a different value.
        /// </summary>
        /// <param name="component">The component with the property value that is to be set.</param>
        /// <param name="value">The new value.</param>
        public override void SetValue(Object component, Object value)
        {
            this.propertyInfo.SetValue(component, value, null);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        /// <summary>
        /// When overridden in a derived class, gets the type of the property.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Type"/> that represents the type of the property.</returns>
        public override Type PropertyType
        {
            get { return this.propertyInfo.PropertyType; }
        }

        /// <summary>
        /// When overridden in a derived class, gets the type of the component this property is bound to.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Type"/> that represents the type of component this property is bound to. When the <see cref="M:System.ComponentModel.PropertyDescriptor.GetValue(System.Object)"/> or <see cref="M:System.ComponentModel.PropertyDescriptor.SetValue(System.Object,System.Object)"/> methods are invoked, the object specified might be an instance of this type.</returns>
        public override Type ComponentType
        {
            get { return this.propertyInfo.DeclaringType; }
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// 	<c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            var descriptor = obj as CustomPropertyDescriptor;
            return ((descriptor != null) && descriptor.propertyInfo.Equals(this.propertyInfo));
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override Int32 GetHashCode()
        {
            return this.propertyInfo.GetHashCode();
        }


        public override void AddValueChanged(object component, EventHandler handler)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            if (typeof(INotifyPropertyChanged).IsAssignableFrom(component.GetType()))
            {
                EventDescriptor eventDesc = TypeDescriptor.GetEvents(typeof (INotifyPropertyChanged))["PropertyChanged"];

                if (eventDesc != null) eventDesc.AddEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
            }
            base.AddValueChanged(component, handler);
        }



        public override void RemoveValueChanged(object component, EventHandler handler)
        {
            if (component == null)
            {
                throw new ArgumentNullException("component");
            }
            if (handler == null)
            {
                throw new ArgumentNullException("handler");
            }

            if (typeof(INotifyPropertyChanged).IsAssignableFrom(this.ComponentType))
            {
                EventDescriptor eventDesc = TypeDescriptor.GetEvents(typeof (INotifyPropertyChanged))["PropertyChanged"];

                if (eventDesc != null) eventDesc.RemoveEventHandler(component, new PropertyChangedEventHandler(this.OnINotifyPropertyChanged));
            }
            base.RemoveValueChanged(component, handler);
        }






        public virtual void OnINotifyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.PropertyName) || (string.Compare(e.PropertyName, this.Name, true, CultureInfo.InvariantCulture) == 0))
            {
                this.OnValueChanged(sender, new PropertyChangedEventArgs(this.Name));
            }
        }
    }
}