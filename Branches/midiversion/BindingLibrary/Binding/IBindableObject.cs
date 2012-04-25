using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace BindingLibrary
{
    public interface IBindableObject
    {
        string BindingId { get; }
        object BindingObject { get; }
    }

    
}
