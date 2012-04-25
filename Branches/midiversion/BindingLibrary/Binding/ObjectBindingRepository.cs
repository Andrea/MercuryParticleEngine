using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace BindingLibrary
{
    public class ObjectBindingRepository : IXmlSerializable

    {
        private readonly IList<IObjectBinding> _repository;
       

        public ObjectBindingRepository()
        {
            _repository = new BindingList<IObjectBinding>();

            Path = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "ObjectBindingRepository.xml");

        }


        public IList<IObjectBinding> Repository 
        { 
            get
            {
                return _repository;
            } 
        }

        public string Path { get; set; }

        public IObjectBinding GetObjectBinding(int index)
        {
            return (index >= 0 && index < _repository.Count) ? _repository[index] : null;
        }

        public int GetObjectBindingIndex(IObjectBinding binding)
        {
            if (binding != null && binding is ObjectBinding)
            {
                return _repository.IndexOf(binding as ObjectBinding);
            }
            return -1;
        }

        public void AddObjectBinding(IObjectBinding binding)
        {
            if (binding == null) return;

            if (!Repository.Contains(binding))
            {
                Repository.Add(binding);
            }
        }

        public IObjectBinding AddNewObjectBinding()
        {
            var element = new ObjectBinding();

            Repository.Add(element);

            return element;
        }

        public void RemoveObjectBinding(IObjectBinding binding)
        {
            if (binding == null) return;

            if (Repository.Contains(binding))
            {
                int index = GetObjectBindingIndex(binding);
                SetBindingSource(index, null, null);
                SetBindingTarget(index, null, null);

                Repository.Remove(binding);
            }
        }

        public void SetBindingSource(int index, IBindableObject bindableObject, string propertyDescriptor)
        {
            var binding = GetObjectBinding(index) as ObjectBinding;

            if (binding != null)
            {
                binding.SetSourceProperty(bindableObject, propertyDescriptor);
            }
        }

        public void SetBindingTarget(int index, IBindableObject bindableObject, string propertyDescriptor)
        {
            var binding = GetObjectBinding(index) as ObjectBinding;

            if (binding != null)
            {
                binding.SetTargetProperty(bindableObject, propertyDescriptor);
            }
        }

        public void SetBindingConverter(int index, ObjectBindingConverter converter)
        {
            var binding = GetObjectBinding(index) as ObjectBinding;

            if (binding != null)
            {
                binding.SetConverter(converter);
            }
        }


        public void Load()
        {
            try
            {
                if (File.Exists(Path))
                {

                    StreamReader sr = new StreamReader(Path);

                    XmlTextReader xmlReader = new XmlTextReader(sr);
                    xmlReader.WhitespaceHandling = WhitespaceHandling.Significant;
                    xmlReader.Normalization = true;
                    xmlReader.XmlResolver = null;


                    try
                    {

                        ReadXml(xmlReader);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }

                    sr.Close();
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }


        public void Save()
        {

            if (!String.IsNullOrEmpty(Path))
            {
                // serialize
                XmlSerializer xs = new XmlSerializer(typeof (ObjectBindingRepository));

                StreamWriter sw = new StreamWriter(Path);
                xs.Serialize(sw, this);

                sw.Close();
            }
        }

        public void ClearRepository()
        {
            foreach (IObjectBinding binding in Repository)
            {
                int index = GetObjectBindingIndex(binding);
                SetBindingSource(index, null, null);
                SetBindingTarget(index, null, null);
            }

            Repository.Clear();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "ObjectBindingRepository")
            {
                reader.Read(); // skip to next node
            }

            while (reader.MoveToContent() == XmlNodeType.Element && reader.LocalName == "Item")
            {
                var item = new ObjectBinding();

                item.ReadXml(reader);

                AddObjectBinding(item);

                reader.Read();
            }


        }

        public void WriteXml(XmlWriter writer)
        {

            foreach (ObjectBinding item in Repository)
            {
                writer.WriteStartElement("Item");
                
                item.WriteXml(writer);
                
                writer.WriteEndElement();
            }

        }

    }
}
