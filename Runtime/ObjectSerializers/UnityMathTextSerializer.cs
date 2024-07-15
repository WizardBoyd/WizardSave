using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using WizardSave.Utils;

namespace WizardSave.ObjectSerializers
{
    public class UnityMathTextSerializer : 
        ITextSerializer<Color>,
        ITextSerializer<Quaternion>,
        ITextSerializer<Matrix4x4>,
        ITextSerializer<Plane>,
        ITextSerializer<Ray>, ITextSerializer<Ray2D>,
        ITextSerializer<Rect>, ITextSerializer<RectInt>,
        ITextSerializer<RangeInt>,
        ITextSerializer<Vector2>, ITextSerializer<Vector2Int>,
        ITextSerializer<Vector3>, ITextSerializer<Vector3Int>,
        ITextSerializer<Vector4>
    {
        public const char NumberSeparator = ',';
        
        public void RegisterInSerializerMap(ObjectSerializerMap serializerMap)
        {
            serializerMap.SetSerializer<Color>(this);
            serializerMap.SetSerializer<Quaternion>(this);
            serializerMap.SetSerializer<Matrix4x4>(this);
            serializerMap.SetSerializer<Plane>(this);
            serializerMap.SetSerializer<Ray>(this);
            serializerMap.SetSerializer<Ray2D>(this);
            serializerMap.SetSerializer<Rect>(this);
            serializerMap.SetSerializer<RectInt>(this);
            serializerMap.SetSerializer<RangeInt>(this);
            serializerMap.SetSerializer<Vector2>(this);
            serializerMap.SetSerializer<Vector2Int>(this);
            serializerMap.SetSerializer<Vector3>(this);
            serializerMap.SetSerializer<Vector3Int>(this);
            serializerMap.SetSerializer<Vector4>(this);
        }
        
        public string SerializeObject(Color obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Color obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(Quaternion obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Quaternion obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(Matrix4x4 obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Matrix4x4 obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(Plane obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Plane obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(Ray obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Ray obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(Ray2D obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Ray2D obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(Rect obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Rect obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(RectInt obj) => SerializeInts(obj);

        public bool TryDeserializeObject(string data, out RectInt obj) => TryDeserializeInts(data, out obj);

        public string SerializeObject(RangeInt obj) => SerializeInts(obj);

        public bool TryDeserializeObject(string data, out RangeInt obj) => TryDeserializeInts(data, out obj);

        public string SerializeObject(Vector2 obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Vector2 obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(Vector2Int obj) => SerializeInts(obj);

        public bool TryDeserializeObject(string data, out Vector2Int obj) => TryDeserializeInts(data, out obj);

        public string SerializeObject(Vector3 obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Vector3 obj) => TryDeserializeFloats(data, out obj);

        public string SerializeObject(Vector3Int obj) => SerializeInts(obj);

        public bool TryDeserializeObject(string data, out Vector3Int obj) => TryDeserializeInts(data, out obj);

        public string SerializeObject(Vector4 obj) => SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Vector4 obj) => TryDeserializeFloats(data, out obj);

        #region Internal unsafe serialization methods

        unsafe private static string SerializeInts<T>(T value) where T : struct
        {
            int intCount = UnsafeUtility.SizeOf<T>() / UnsafeUtility.SizeOf<int>();
            void* ptr = UnsafeUtility.AddressOf(ref value);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(UnsafeUtility.ReadArrayElement<int>(ptr, 0).ToString(CultureInfo.InvariantCulture));
            for (int i = 1; i < intCount; i++)
            {
                stringBuilder.Append(NumberSeparator);
                stringBuilder.Append(UnsafeUtility.ReadArrayElement<int>(ptr, i).ToString(CultureInfo.InvariantCulture));
            }

            return stringBuilder.ToString();
        }
        
        unsafe private static bool TryDeserializeInts<T>(string text, out T value) where T : struct
        {
            value = default;
            void* ptr = UnsafeUtility.AddressOf(ref value);
            int intCount = UnsafeUtility.SizeOf<T>() / UnsafeUtility.SizeOf<int>();

            using (IEnumerator<int> enumerator = text.EnumerateInts(NumberSeparator).GetEnumerator())
                for (int i = 0; i < intCount; i++)
                {
                    if (!enumerator.MoveNext())
                    {
                        return false;
                    }
                    UnsafeUtility.WriteArrayElement(ptr, i, enumerator.Current);
                }
            return true;
        }

        unsafe private static string SerializeFloats<T>(T value) where T : struct
        {
            int floatCount = UnsafeUtility.SizeOf<T>() / UnsafeUtility.SizeOf<float>();
            void* ptr = UnsafeUtility.AddressOf(ref value);

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(UnsafeUtility.ReadArrayElement<float>(ptr, 0).ToString(CultureInfo.InvariantCulture));
            for (int i = 1; i < floatCount; i++)
            {
                stringBuilder.Append(NumberSeparator);
                stringBuilder.Append(UnsafeUtility.ReadArrayElement<float>(ptr, i).ToString(CultureInfo.InvariantCulture));
            }

            return stringBuilder.ToString();
        }

        unsafe private static bool TryDeserializeFloats<T>(string text, out T value) where T : struct
        {
            value = default;
            void* ptr = UnsafeUtility.AddressOf(ref value);
            int floatCount = UnsafeUtility.SizeOf<T>() / UnsafeUtility.SizeOf<float>();

            using (IEnumerator<float> enumerator = text.EnumerateFloats(NumberSeparator).GetEnumerator())
                for (int i = 0; i < floatCount; i++)
                {
                    if (!enumerator.MoveNext())
                    {
                        return false;
                    }
                    UnsafeUtility.WriteArrayElement(ptr, i, enumerator.Current);
                }
            return true;
        }

        #endregion
    }
}