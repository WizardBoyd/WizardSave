using System;

namespace WizardSave.ObjectSerializers
{
    public interface IBinarySerializer : IObjectSerializer
    {
        byte[] SerializeObject<T>(T obj);
        
        byte[] SerializeObject(object obj);
        
        bool TryDeserializeObject<T>(byte[] data, out T obj);
        
        bool TryDeserializeObject(byte[] data,Type type, out object obj);
    }
    
    public interface IBinarySerializer<T> : IBinarySerializer
    {
        byte[] SerializeObject(T obj);
        bool TryDeserializeObject(byte[] data, out T obj);
    }
}