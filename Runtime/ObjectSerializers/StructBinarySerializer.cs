using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

#if UNITY_2018_1_OR_NEWER
namespace WizardSave.ObjectSerializers
{
    public class StructBinarySerializer : IBinarySerializer
    {
        public unsafe byte[] SerializeObject<T>(T obj)
        {
            Debug.AssertFormat(UnsafeUtility.IsUnmanaged(typeof(T)), "Type {0} is not unmanaged", typeof(T).Name);
            int size = UnsafeUtility.SizeOf(typeof(T));
            byte[] data = new byte[size];
            fixed(void* ptr = data)
            {
                UnsafeUtility.MemCpy(ptr, UnsafeUtility.AddressOf(ref UnsafeUtility.As<T,int>(ref obj)), size);
            }

            return data;
        }

        public unsafe byte[] SerializeObject(object obj)
        {
            Debug.AssertFormat(UnsafeUtility.IsUnmanaged(obj.GetType()), "Type {0} is not unmanaged", obj.GetType().Name);
            int size = UnsafeUtility.SizeOf(obj.GetType());
            byte[] data = new byte[size];
            fixed(void* ptr = data)
            {
                UnsafeUtility.MemCpy(ptr, UnsafeUtility.AddressOf(ref UnsafeUtility.As<object,int>(ref obj)), size);
            }

            return data;
        }

        public unsafe bool TryDeserializeObject<T>(byte[] bytes, out T value)
        {
            Debug.AssertFormat(UnsafeUtility.IsUnmanaged(typeof(T)), "Type {0} is not unmanaged", typeof(T).Name);
            value = default;
            int sizeOfT = UnsafeUtility.SizeOf(typeof(T));
            if (bytes.Length < sizeOfT)
                return false;
            
            fixed(void* bufferPtr = bytes)
            {
                UnsafeUtility.MemCpy(UnsafeUtility.AddressOf(ref UnsafeUtility.As<T, int>(ref value)), bufferPtr, sizeOfT);
            }

            return true;
        }

        [Obsolete("This method is not implemented yet.")]
        public unsafe bool TryDeserializeObject(byte[] data, Type type, out object obj)
        {
            throw new NotImplementedException("This method is not implemented yet.");
            //TODO: There is a bug that causes the unsafe code to throw a unhandled exception causing the program to crash
            Debug.AssertFormat(UnsafeUtility.IsUnmanaged(type), "Type {0} is not unmanaged", type.Name);
            int sizeOfType = UnsafeUtility.SizeOf(type);
            obj = default;
            if(data.Length < sizeOfType)
                return false;

            fixed (void* bufferPtr = data)
            {
                void* objPtr = UnsafeUtility.AddressOf(ref UnsafeUtility.As<object, int>(ref obj));
                UnsafeUtility.MemCpy(objPtr, bufferPtr, sizeOfType);
            }

            return true;
        }
    }
}
#endif