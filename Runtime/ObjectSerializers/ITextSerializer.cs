using System;

namespace WizardSave.ObjectSerializers
{
    public interface ITextSerializer : IObjectSerializer
    {
        string SerializeObject<T>(T obj);
        string SerializeObject(object obj);
        bool TryDeserializeObject<T>(string data, out T obj);
        
        bool TryDeserializeObject(string data, Type type, out object obj);
    }
    
    public interface ITextSerializer<T> : IObjectSerializer
    {
        string SerializeObject(T obj);
        bool TryDeserializeObject(string data, out T obj);
    }
}