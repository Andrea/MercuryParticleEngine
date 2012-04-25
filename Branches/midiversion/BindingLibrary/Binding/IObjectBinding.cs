using System.ComponentModel;
using System.Xml.Serialization;

namespace BindingLibrary
{
    public interface IObjectBinding : INotifyPropertyChanged, IXmlSerializable
    {
        IBindableObject SourceObject { get; }
        string SourceProperty { get; }

        IBindableObject TargetObject { get; }
        string TargetProperty { get; }

        ObjectBindingConverter BindingConverter { get; }

        string Name { get; }
    }
}