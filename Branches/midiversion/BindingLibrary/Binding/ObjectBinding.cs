using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BindingLibrary
{

  

    internal class ObjectBinding : IDisposable, IObjectBinding
    {

        #region Fields

        // IBindable object and string property representation
        private string sourceProperty;
        private IBindableObject sourceObject;

        // IBindable object and string propertyRepresentation    
        private string targetProperty;
        private IBindableObject targetObject;

        private ObjectBindingConverter bindingConverter;

        #endregion

    
        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        
        #region Initialization / Dispose

        public ObjectBinding()
        {
            bindingConverter = new ObjectBindingConverter();
        }

        public void Dispose()
        {
        }


        #endregion


        #region Public Properties


        #region Source

        public void SetSourceProperty(IBindableObject bindableObject, string property)
        {
            // Unbind previous
            UnBind(SourceObject, SourceProperty, Source_PropertyChanged);

            // store
            SourceObject = bindableObject;
            SourceProperty = property;

            // bind
            Bind(SourceObject, SourceProperty, Source_PropertyChanged);

            UpdateTarget();


        }

        public IBindableObject SourceObject
        {
            get
            {
                return sourceObject;
            }
            private set
            {
                sourceObject = value;

                OnPropertyChanged(new PropertyChangedEventArgs("SourceObject"));

            }
        }

        public string SourceProperty
        {
            get
            {
                return sourceProperty;
            }
            private set
            {

                //TypeDescriptor.GetProperties(sourceObject)[propertyName];
                sourceProperty = value;

                OnPropertyChanged(new PropertyChangedEventArgs("SourceProperty"));
            }
        }


        #endregion

        #region Target

        public void SetTargetProperty(IBindableObject bindableObject, string property)
        {
            TargetObject = bindableObject;
            TargetProperty = property;

            if (TargetObject != null)
            {
                UpdateTarget();
            }
        }

        public IBindableObject TargetObject
        {
            get
            {
                return targetObject;
            }
            private set
            {

                targetObject = value;

                OnPropertyChanged(new PropertyChangedEventArgs("TargetObject"));
            }
        }

        public string TargetProperty
        {
            get
            {
                return targetProperty;

            }
            private set
            {

                targetProperty = value;

                OnPropertyChanged(new PropertyChangedEventArgs("TargetProperty"));
            }
        }

        #endregion


        public void SetConverter(ObjectBindingConverter converter)
        {
            if (converter != null)
            {
                BindingConverter = converter;
            }
        }


        public ObjectBindingConverter BindingConverter
        {
            get
            {
                return bindingConverter;
            }
            private set
            {
                bindingConverter = value;

                OnPropertyChanged(new PropertyChangedEventArgs("BindingConverter"));
            }
        }

        public string Name
        {
            get
            {

                // Source
                string componentName = (SourceObject != null && SourceObject.BindingObject != null) ? SourceObject.BindingObject.ToString() : "<Empty>";

                componentName += (SourceProperty != null ? " (" + SourceProperty + ")" : "");

                componentName += "  -->  ";

                // Target
                componentName += (TargetObject != null && TargetObject.BindingObject != null) ? TargetObject.BindingObject.ToString() : "<Empty>";

                componentName += (TargetProperty != null ? " (" + TargetProperty + ")" : "");

               
                return componentName;
            }

        }



        #endregion


        #region IXmlSerializable


        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element)
            {
                reader.Read();

                // Binding
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Converter")
                {
                    bindingConverter.SourceMin = Convert.ToSingle(reader["SourceMin"]);

                    bindingConverter.SourceMax = Convert.ToSingle(reader["SourceMax"]);

                    bindingConverter.TargetMin = Convert.ToSingle(reader["TargetMin"]);

                    bindingConverter.TargetMax = Convert.ToSingle(reader["TargetMax"]);

                    reader.Read();
                }

                // SOURCE
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Source")
                {

                    IBindableObject obj = BindableObjectRepository.GetBindableObject(reader["BindingId"]);

                    if (obj != null)
                    {

                        // current property
                        string propertyName = reader["PropertyName"];

                        if (!String.IsNullOrEmpty(propertyName))
                        {
                            SetSourceProperty(obj, propertyName);
                        }
                    }

                    reader.Read();
                }


                // TARGET
                if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Target")
                {
                    IBindableObject obj = BindableObjectRepository.GetBindableObject(reader["BindingId"]);

                    if (obj != null)
                    {

                        // current property
                        string propertyName = reader["PropertyName"];

                        if (!String.IsNullOrEmpty(propertyName))
                        {
                            SetTargetProperty(obj, propertyName);
                        }
                    }

                    reader.Read();
                }
               
               

            }
        }



        public void WriteXml(XmlWriter writer)
        {
            writer.WriteStartElement("Converter");

            if (BindingConverter != null)
            {
                writer.WriteAttributeString("SourceMin", BindingConverter.SourceMin.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("SourceMax", BindingConverter.SourceMax.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("TargetMin", BindingConverter.TargetMin.ToString(CultureInfo.InvariantCulture));
                writer.WriteAttributeString("TargetMax", BindingConverter.TargetMax.ToString(CultureInfo.InvariantCulture));

            }
            writer.WriteEndElement();


            writer.WriteStartElement("Source");
            if (SourceObject != null && !String.IsNullOrEmpty(SourceObject.BindingId))
            {
                writer.WriteAttributeString("BindingId", SourceObject.BindingId);

                writer.WriteAttributeString("PropertyName", SourceProperty ?? "");

            }

            writer.WriteEndElement();

            writer.WriteStartElement("Target");

            if (TargetObject != null && !String.IsNullOrEmpty(TargetObject.BindingId))
            {
                writer.WriteAttributeString("BindingId", TargetObject.BindingId);

                writer.WriteAttributeString("PropertyName", TargetProperty ?? "");

            }
            writer.WriteEndElement();

         

            

        }


        #endregion


        #region Protected Methods

        protected virtual void OnPropertyChanged(PropertyChangedEventArgs args)
        {
            if (PropertyChanged != null) PropertyChanged(this, args);
        }

        #endregion


        #region Private Methods


       


        private static void Bind(IBindableObject bindableObject, string propertyName, EventHandler OnPropertyDescriptorValueChanged)
        {
            try
            {

                object rootObject;
                PropertyDescriptor propertyDescriptor;

                if (ResolveDescriptor(bindableObject, propertyName, out propertyDescriptor, out rootObject))
                {
                    propertyDescriptor.AddValueChanged(rootObject, OnPropertyDescriptorValueChanged);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }



        private static void UnBind(IBindableObject bindableObject, string propertyName, EventHandler OnPropertyDescriptorValueChanged)
        {
            try
            {

                object rootObject;
                PropertyDescriptor propertyDescriptor;

                if (ResolveDescriptor(bindableObject, propertyName, out propertyDescriptor, out rootObject))
                {
                    propertyDescriptor.RemoveValueChanged(rootObject, OnPropertyDescriptorValueChanged);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }


        /// <summary>
        /// This method resolves the descriptor and its root object from a bindable object and its propertyName
        /// 
        /// i.e.
        /// IBindable Employee 
        /// String Person.Location.X
        /// 
        /// resolves to
        /// 
        /// Object Location  
        /// PropertyDescriptor X
        /// 
        //
        /// </summary>
        /// <param name="bindableObject"></param>
        /// <param name="propertyName"></param>
        /// <param name="propertyDescriptor"></param>
        /// <param name="propertyDescriptorRoot"></param>
        /// <returns></returns>
        private static bool ResolveDescriptor(IBindableObject bindableObject, string propertyName, out PropertyDescriptor propertyDescriptor, out object propertyDescriptorRoot)
        {

            propertyDescriptor = null;
            propertyDescriptorRoot = null;


            if (bindableObject == null || String.IsNullOrEmpty(propertyName))
            {
                return false;
            }

            bool success = true;


            propertyDescriptorRoot = bindableObject;

            string[] properties = propertyName.Split('.');

         
            for (int i = 0; i < properties.Length; i++)
            {
                string name = properties[i];

                if (propertyDescriptorRoot != null)
                {

                    
                    // get the descriptor from the root
                    
                    propertyDescriptor = FindPropertyDescriptor(propertyDescriptorRoot, name);

                    if (propertyDescriptor == null) success = false;

                    // recalc the root Obj
                    if (propertyDescriptor != null && i < properties.Length - 1)
                    {
                        propertyDescriptorRoot = propertyDescriptor.GetValue(propertyDescriptorRoot);
                    }
                }
                else
                {
                    success = false;
                }
            }

            return success;


        }

        private static PropertyDescriptor FindPropertyDescriptor(object propertyDescriptorRoot, string name)
        {
            if (String.IsNullOrEmpty(name)) return null;

            PropertyDescriptor descriptor = null;

            // check if there is a type converter describing the properties
            TypeConverter typeConverter = TypeDescriptor.GetConverter(propertyDescriptorRoot);
            if (typeConverter != null && typeConverter.GetPropertiesSupported())
            {
                // get the propertycollection of the converter
                PropertyDescriptorCollection propertyDescriptorCollection = typeConverter.GetProperties(propertyDescriptorRoot);
                if (propertyDescriptorCollection!= null)
                {
                    // search for the property
                    descriptor = propertyDescriptorCollection[name];
                }
            }

            
            return descriptor ?? (TypeDescriptor.GetProperties(propertyDescriptorRoot)[name]);
        }



        private void Source_PropertyChanged(object sender, EventArgs e)
        {
            // Update the the target
            UpdateTarget();
        }

        private void UpdateTarget()
        {

            object srcPropDescParent;
            PropertyDescriptor srcPropDesc;
            bool sourceResolved = ResolveDescriptor(SourceObject, SourceProperty, out srcPropDesc, out srcPropDescParent);

            object targetPropDescParent;
            PropertyDescriptor targetPropDesc;
            bool targetResolved = ResolveDescriptor(TargetObject, TargetProperty, out targetPropDesc, out targetPropDescParent);

            if (sourceResolved && targetResolved)
            {
                try
                {

                    if (bindingConverter.CanConvert(srcPropDesc.PropertyType, targetPropDesc.PropertyType))
                    {
                        object sourceValue = srcPropDesc.GetValue(srcPropDescParent);
                        object targetValue = bindingConverter.Convert(sourceValue, targetPropDesc.PropertyType);

                        if (targetValue != null)
                        {
                            // update the target
                            targetPropDesc.SetValue(targetPropDescParent, targetValue);

                            // if the updated property is updated on a ValueType object -> Update the parent property also.
                            SetParentValue(targetPropDescParent, targetPropDesc, TargetObject, TargetProperty);

                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }

        private static void SetParentValue(object parent, PropertyDescriptor parentProperty, IBindableObject bindableObject, string bindableObjectPropertyName)
        {
            if (parent == null || parentProperty == null|| bindableObject == null || string.IsNullOrEmpty(bindableObjectPropertyName)) return;

                // if the updated property is updated on a ValueType object -> Update the parent property also.
            if (parent.GetType().IsValueType && bindableObjectPropertyName.EndsWith(parentProperty.Name))
            {
                object parentObj;
                PropertyDescriptor parentDescriptor;

                int index = bindableObjectPropertyName.Length - parentProperty.Name.Length - 1 /* remove point*/;

                if (index > 0)
                {
                    if (ResolveDescriptor(bindableObject, bindableObjectPropertyName.Remove(index), out parentDescriptor,
                                          out parentObj))
                    {
                        parentDescriptor.SetValue(parentObj, parent);

                        SetParentValue(parentObj, parentDescriptor, bindableObject, bindableObjectPropertyName.Remove(index));
                    }
                }

            }
        }

        #endregion

    }
}