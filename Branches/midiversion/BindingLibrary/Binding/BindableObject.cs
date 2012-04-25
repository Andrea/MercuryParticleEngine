using System;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BindingLibrary
{
    public class BindableObject : IBindableObject, IXmlSerializable
    {
        private string _bindingId;

        public BindableObject():this(null)
        {
        }

        public BindableObject(object bindingObject) 
        {
            BindingId = Guid.NewGuid().ToString();

            BindingObject = bindingObject ?? this;
        }

        public BindableObject(string id, object bindingObject)
        {
            BindingId = String.IsNullOrEmpty(id) ? Guid.NewGuid().ToString() : id;

            BindingObject = bindingObject ?? this;
        }



        public virtual string BindingId
        {
            get { return _bindingId; }
            set
            {
                if (value != _bindingId && value != null)
                {
                    BindableObjectRepository.UnRegisterBindableObject(this);

                    _bindingId = value;

                    BindableObjectRepository.RegisterBindableObject(this);
                }
            }
        }


        public object BindingObject { get; private set; }



        #region IXmlSerializable

        public virtual XmlSchema GetSchema()
        {
            return null;
        }

        public virtual void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element)
            {
                string value = reader["BindingId"];
                if (!String.IsNullOrEmpty(value))
                {
                    BindingId = value;
                }
            }
            
        }

        public virtual void WriteXml(XmlWriter writer)
        {
            writer.WriteAttributeString("BindingId", BindingId);
        }

        #endregion
    }
}
