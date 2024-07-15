namespace WizardSave.ObjectSerializers
{
    public interface IBinarySerializer : IObjectSerializer
    {
        byte[] SerializeObject<T>(T obj);
        bool TryDeserializeObject<T>(byte[] data, out T obj);
    }
    
    public interface IBinarySerializer<T> : IBinarySerializer
    {
        byte[] SerializeObject(T obj);
        bool TryDeserializeObject(byte[] data, out T obj);
    }
}