using System;
using WizardSave.ObjectSerializers;

namespace WizardSave
{
     public static partial class IKeyValueStoreExtensions
    {
        public static bool TryGetObject<T>(this IKeyValueStore kvs, string key, out T value)
        {
            return TryGetObject(kvs, ObjectSerializerMap.DefaultSerializerMap, key, out value);
        }
        public static bool TryGetObject<T>(this IKeyValueStore kvs, ObjectSerializerMap serializerMap, string key, out T value)
        {
            switch (serializerMap.GetSerializer<T>())
            {
                case ITextSerializer<T> specializedTextSerializer:
                    return kvs.TryGetObject(specializedTextSerializer, key, out value);
                case IBinarySerializer<T> specializedBinarySerializer:
                    return kvs.TryGetObject(specializedBinarySerializer, key, out value);
                case ITextSerializer textSerializer:
                    return kvs.TryGetObject(textSerializer, key, out value);
                case IBinarySerializer binarySerializer:
                    return kvs.TryGetObject(binarySerializer, key, out value);
                default:
                    throw new InvalidCastException("Expected default object serializer to be either ITextSerializer or IBinarySerializer type.");
            }
        }
        public static bool TryGetObject<T>(this IKeyValueStore kvs, ITextSerializer serializer, string key, out T value)
        {
            value = default;
            return kvs.TryGetString(key, out string s)
                && serializer.TryDeserializeObject(s, out value);
        }
        public static bool TryGetObject<T>(this IKeyValueStore kvs, ITextSerializer<T> serializer, string key, out T value)
        {
            value = default;
            return kvs.TryGetString(key, out string s)
                && serializer.TryDeserializeObject(s, out value);
        }
        public static bool TryGetObject<T>(this IKeyValueStore kvs, IBinarySerializer serializer, string key, out T value)
        {
            value = default;
            return kvs.TryGetBytes(key, out byte[] b)
                && serializer.TryDeserializeObject(b, out value);
        }
        public static bool TryGetObject<T>(this IKeyValueStore kvs, IBinarySerializer<T> serializer, string key, out T value)
        {
            value = default;
            return kvs.TryGetBytes(key, out byte[] b)
                && serializer.TryDeserializeObject(b, out value);
        }

        public static T GetObject<T>(this IKeyValueStore kvs, string key, T defaultValue = default)
        {
            return kvs.TryGetObject(key, out T value) ? value : defaultValue;
        }
        public static T GetObject<T>(this IKeyValueStore kvs, ITextSerializer serializer, string key, T defaultValue = default)
        {
            return kvs.TryGetObject(serializer, key, out T value) ? value : defaultValue;
        }
        public static T GetObject<T>(this IKeyValueStore kvs, ITextSerializer<T> serializer, string key, T defaultValue = default)
        {
            return kvs.TryGetObject(serializer, key, out T value) ? value : defaultValue;
        }
        public static T GetObject<T>(this IKeyValueStore kvs, IBinarySerializer serializer, string key, T defaultValue = default)
        {
            return kvs.TryGetObject(serializer, key, out T value) ? value : defaultValue;
        }
        public static T GetObject<T>(this IKeyValueStore kvs, IBinarySerializer<T> serializer, string key, T defaultValue = default)
        {
            return kvs.TryGetObject(serializer, key, out T value) ? value : defaultValue;
        }


        public static void SetObject<T>(this IKeyValueStore kvs, string key, T value)
        {
            kvs.SetObject(ObjectSerializerMap.DefaultSerializerMap, key, value);
        }
        public static void SetObject<T>(this IKeyValueStore kvs, ObjectSerializerMap serializerMap, string key, T value)
        {
            switch (serializerMap.GetSerializer<T>())
            {
                case ITextSerializer<T> specializedTextSerializer:
                    kvs.SetObject(specializedTextSerializer, key, value);
                    break;
                case IBinarySerializer<T> specializedBinarySerializer:
                    kvs.SetObject(specializedBinarySerializer, key, value);
                    break;
                case ITextSerializer textSerializer:
                    kvs.SetObject(textSerializer, key, value);
                    break;
                case IBinarySerializer binarySerializer:
                    kvs.SetObject(binarySerializer, key, value);
                    break;
                default:
                    throw new InvalidCastException("Expected default object serializer to be either ITextSerializer or IBinarySerializer type.");
            }
        }
        public static void SetObject<T>(this IKeyValueStore kvs, ITextSerializer serializer, string key, T value)
        {
            kvs.SetString(key, serializer.SerializeObject(value));
        }
        public static void SetObject<T>(this IKeyValueStore kvs, ITextSerializer<T> serializer, string key, T value)
        {
            kvs.SetString(key, serializer.SerializeObject(value));
        }
        public static void SetObject<T>(this IKeyValueStore kvs, IBinarySerializer serializer, string key, T value)
        {
            kvs.SetBytes(key, serializer.SerializeObject(value));
        }
        public static void SetObject<T>(this IKeyValueStore kvs, IBinarySerializer<T> serializer, string key, T value)
        {
            kvs.SetBytes(key, serializer.SerializeObject(value));
        }
    }
}