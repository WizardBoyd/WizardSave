namespace WizardSave.ObjectSerializers
{
    public interface ITextSerializer : IObjectSerializer
    {
        string SerializeObject<T>(T obj);
        bool TryDeserializeObject<T>(string data, out T obj);
    }
    
    public interface ITextSerializer<T> : IObjectSerializer
    {
        string SerializeObject(T obj);
        bool TryDeserializeObject(string data, out T obj);
    }
}