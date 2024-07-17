using System;
using JetBrains.Annotations;
using WizardSave.Enums;

namespace WizardSave.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    public class KvsPropertyAttribute : Attribute
    {
        public Type KvsType { get; }
        
        public string Path { get; }
        public string PropertyName { get;}
        
        public KvsPropertyAttribute(Type kvsType, string path, string propertyName)
        {
            if(string.IsNullOrEmpty(propertyName))
                throw new ArgumentNullException(nameof(propertyName));
            if(!IsValid(kvsType))
                throw new ArgumentException("KvsType must implement IKeyValueStore");
            if(!IsConcreteType(kvsType))
                throw new ArgumentException("KvsType must be a concrete class");
            if(string.IsNullOrEmpty(path))
                throw new ArgumentNullException(nameof(path));
            PropertyName = propertyName;
            Path = path;
            KvsType = kvsType;
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