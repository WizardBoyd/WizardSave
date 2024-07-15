using System.Collections.Generic;
using System;

namespace WizardSave.ObjectSerializers
{
    public class ObjectSerializerMap
    { 
        public static ObjectSerializerMap DefaultSerializerMap { get; } = new ObjectSerializerMap();
        
        public IDictionary<Type, IObjectSerializer> TypeToSerializeMap { get; set; }

        public IObjectSerializer DefaultSerializer
        {
            get => m_defaultSerializer;
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(DefaultSerializer));
                m_defaultSerializer = value;
            }
        }
        private IObjectSerializer m_defaultSerializer;
        
        public ObjectSerializerMap()
        {
            TypeToSerializeMap = new Dictionary<Type, IObjectSerializer>();
            DefaultSerializer = new JsonUtilityTextSerializer();
        }
        
        public IObjectSerializer GetSerializer(Type type)
        {
            if (TypeToSerializeMap.TryGetValue(type, out var serializer))
                return serializer;
            return DefaultSerializer;
        }
        
        public IObjectSerializer GetSerializer<T>()
        {
            return (IObjectSerializer)GetSerializer(typeof(T));
        }
        
        public void SetSerializer(Type type, IObjectSerializer serializer)
        {
            if (serializer == null)
                throw new ArgumentNullException(nameof(serializer));
            TypeToSerializeMap[type] = serializer;
        }
        
        public void SetSerializer<T>(IObjectSerializer serializer)
        {
            TypeToSerializeMap[typeof(T)] = serializer;
        }
    }
}