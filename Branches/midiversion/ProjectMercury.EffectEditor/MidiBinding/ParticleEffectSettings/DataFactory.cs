using System;
using System.IO;
using System.Xml.Serialization;

namespace BindingLibrary
{
    public class DataFactory : IBindableObjectFactory
    {
        private static DataFactory _mInstance;
        private LayerProperties layerProperties;

        protected DataFactory()
        {
        }

        /// <summary>
        /// Get the an instance of MidiDeviceListModel
        /// </summary>
        public static DataFactory Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    _mInstance = new DataFactory();
                }
                return _mInstance;
            }
        }


        public IBindableObject CreateObject(string id)
        {

            switch (id)
            {
                case (LayerProperties.ID):
                    return LayerProperties;
            }
            return null;
        }

        public LayerProperties LayerProperties
        {
            get
            {
                if (layerProperties == null)
                {
                    layerProperties = new LayerProperties();
                }
                return layerProperties;
            }
        }
    }
}