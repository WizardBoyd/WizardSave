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
        public string SerializeObject(Color obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Color obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(Quaternion obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Quaternion obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(Matrix4x4 obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Matrix4x4 obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(Plane obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Plane obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(Ray obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Ray obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(Ray2D obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Ray2D obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(Rect obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Rect obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(RectInt obj) => UnsafeTextSerializerMethods.SerializeInts(obj);

        public bool TryDeserializeObject(string data, out RectInt obj) => UnsafeTextSerializerMethods.TryDeserializeInts(data, out obj);

        public string SerializeObject(RangeInt obj) => UnsafeTextSerializerMethods.SerializeInts(obj);

        public bool TryDeserializeObject(string data, out RangeInt obj) => UnsafeTextSerializerMethods.TryDeserializeInts(data, out obj);

        public string SerializeObject(Vector2 obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Vector2 obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(Vector2Int obj) => UnsafeTextSerializerMethods.SerializeInts(obj);

        public bool TryDeserializeObject(string data, out Vector2Int obj) => UnsafeTextSerializerMethods.TryDeserializeInts(data, out obj);

        public string SerializeObject(Vector3 obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Vector3 obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);

        public string SerializeObject(Vector3Int obj) => UnsafeTextSerializerMethods.SerializeInts(obj);

        public bool TryDeserializeObject(string data, out Vector3Int obj) => UnsafeTextSerializerMethods.TryDeserializeInts(data, out obj);

        public string SerializeObject(Vector4 obj) => UnsafeTextSerializerMethods.SerializeFloats(obj);

        public bool TryDeserializeObject(string data, out Vector4 obj) => UnsafeTextSerializerMethods.TryDeserializeFloats(data, out obj);
    }
}