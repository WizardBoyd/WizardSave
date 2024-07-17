using System;
using System.Reflection;
using UnityEngine;

namespace WizardSave.ObjectSerializers
{
    public class JsonUtilityTextSerializer : ITextSerializer
    {
        public bool PrettyPrint {get; set;}
        
        public string SerializeObject<T>(T obj)
        {
            if(!IsTypeSupported(typeof(T)))
                throw new ArgumentException("Type is not supported");
            return JsonUtility.ToJson(obj, PrettyPrint);
        }

        public string SerializeObject(object obj)
        {
            if(!IsTypeSupported(obj.GetType()))
                throw new ArgumentException("Type is not supported");
            return JsonUtility.ToJson(obj, PrettyPrint);
        }

        public bool TryDeserializeObject<T>(string data, out T obj)
        {
            try
            {
                if(!IsTypeSupported(typeof(T)))
                    throw new ArgumentException("Type is not supported");
                obj = JsonUtility.FromJson<T>(data);
                return true;
            }
            catch(Exception e)
            {
                obj = default;
                return false;
            }
        }

        public bool TryDeserializeObject(string data, Type type, out object obj)
        {
            try
            {
                if(!IsTypeSupported(type))
                    throw new ArgumentException("Type is not supported");
                obj = JsonUtility.FromJson(data, type);
                return true;
            }
            catch(Exception e)
            {
                obj = default;
                return false;
            }
        }
        
        private bool IsTypeSupported(Type type)
        {
            return typeof(MonoBehaviour).IsAssignableFrom(type) || typeof(ScriptableObject).IsAssignableFrom(type) ||
                   (Attribute.IsDefined(type, typeof(SerializableAttribute)) && !type.IsPrimitive && (type.IsClass || type.IsValueType));
        }
    }
}