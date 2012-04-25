namespace BindingLibrary
{
    public interface IBindableObjectFactory
    {
        IBindableObject CreateObject(string id);
    }
}