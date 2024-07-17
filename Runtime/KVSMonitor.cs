using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using WizardSave.Attributes;
using WizardSave.Enums;

namespace WizardSave
{
    public class KVSMonitor : MonoBehaviour
    {
        
        private class KvsStorage
        {
            public string ID { get; }
            public Type Type { get; }
            public object Instance { get; }
            
            public KvsStorage(string id, Type type, object instance = null)
            {
                ID = id;
                Type = type;
                Instance = instance;
            }
            public override int GetHashCode()
            {
                return ID.GetHashCode() + Type.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null || GetType() != obj.GetType())
                {
                    return false;
                }
                KvsStorage other = (KvsStorage) obj;
                return ID == other.ID && Type == other.Type;
            }
        }
        
        private const BindingFlags m_bindingFlags =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
        
        private readonly HashSet<KvsStorage> m_kvsRegistry = new HashSet<KvsStorage>();
        
        public static KVSMonitor Instance { get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                throw new System.Exception("An instance of this singleton already exists");
            }
            else
            {
                Instance = this;
            }
        }

        private void Start()
        {
            PerformInjection();
        }

        private void PerformInjection()
        {
            GatherProviders();

            InjectAll();
        }

        public void InjectAll()
        {
            IEnumerable<MonoBehaviour> injectables = FindObjectsOfType<MonoBehaviour>().Where(IsKVSPropertyAttribute);
            foreach (MonoBehaviour monoBehaviour in injectables)
            {
                Inject(monoBehaviour);
            }
        }

        public void GatherProviders()
        {
            IEnumerable<MonoBehaviour> providers = FindObjectsOfType<MonoBehaviour>().Where(IsKVSSourceAttribute);
            foreach (MonoBehaviour injectable in providers)
            {
                RegisterKVSItem(injectable);
            }
        }

        public void Inject(object injectable)
        {
            Type type = injectable.GetType();
            IEnumerable<FieldInfo> injectableFields = type.GetFields(m_bindingFlags).Where(member => 
                Attribute.IsDefined(member, typeof(Attributes.KvsPropertyAttribute)));

            foreach (FieldInfo injectableField in injectableFields)
            {
                KvsPropertyAttribute injectableAttribute = injectableField.GetCustomAttribute<KvsPropertyAttribute>();
                Type fieldType = injectableField.FieldType;
                IKeyValueStore resolvedInstance = (IKeyValueStore)Resolve(injectableAttribute.KvsType, injectableAttribute.Path);
                if (resolvedInstance == null)
                {
                    throw new Exception("Failed to Retrieve KVS Instance for " + fieldType.Name);
                }
                if (resolvedInstance.TryGetObject(injectableAttribute.PropertyName, fieldType, out object value))
                {
                    injectableField.SetValue(injectable, value);
                    continue;
                }
                resolvedInstance.SetObject(injectableAttribute.PropertyName, fieldType, injectableField.GetValue(injectable));
            }
        }
     

        private void RegisterKVSItem(MonoBehaviour obj)
        {
            if(!IsKVSSourceAttribute(obj))
                return;
            MethodInfo[] methods = obj.GetType().GetMethods(m_bindingFlags);
            
            foreach (MethodInfo info in methods)
            {
                if(!Attribute.IsDefined(info, typeof(KvsSourceAttribute)))
                    continue;
                KvsSourceAttribute attribute = info.GetCustomAttribute<KvsSourceAttribute>();
                if(attribute == null)
                    continue;
                Type returnType = info.ReturnType;
                if(!isKvsItemValid(returnType))
                    continue;
                var providedInstance = info.Invoke(obj, null);
                if (providedInstance != null)
                {
                    m_kvsRegistry.Add(new KvsStorage(attribute.ID, returnType, providedInstance));
                }
            }
        }
        
        private bool isKvsItemValid(Type type)
        {
            if (type == null)
                return false;
            return typeof(IKeyValueStore).IsAssignableFrom(type) && type.IsClass && !type.IsAbstract && !type.IsInterface;
        }

        private object Resolve(Type type, string providerId)
        {
            if(m_kvsRegistry.TryGetValue(new KvsStorage(providerId, type), out var kvsStorage))
                return kvsStorage.Instance;
            return null;
        }
        
        private static bool IsKVSSourceAttribute(MonoBehaviour obj)
        {
            var methods = obj.GetType().GetMethods();
            return methods.Any(method => Attribute.IsDefined(method, typeof(KvsSourceAttribute)));
        }
        
        private static bool IsKVSPropertyAttribute(MonoBehaviour obj)
        {
            MemberInfo[] members = obj.GetType().GetMembers(m_bindingFlags);
            return members.Any(member => Attribute.IsDefined(member, typeof(Attributes.KvsPropertyAttribute)));
        }
    }
}