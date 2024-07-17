using System;
using WizardSave.Enums;

namespace WizardSave.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class KvsSourceAttribute : Attribute
    {
        public Type ProviderType { get; }
        public string ID { get; }
        
        public KvsSourceAttribute(Type providerType, string id)
        {
            if(!IsValid(providerType))
                throw new ArgumentException("KvsType must implement IKeyValueStore");
            if(!IsConcreteType(providerType))
                throw new ArgumentException("KvsType must be a concrete class");
            ProviderType = providerType;
            ID = id;
        }
        
        private bool IsConcreteType(Type type)
        {
            return type.IsClass && !type.IsAbstract && !type.IsInterface;
        }

        private bool IsValid(Type type)
        {
            if (type == null)
                return false;
            return typeof(IKeyValueStore).IsAssignableFrom(type);
        }
    }
}