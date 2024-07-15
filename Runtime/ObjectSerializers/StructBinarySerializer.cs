using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

#if UNITY_2018_1_OR_NEWER
namespace WizardSave.ObjectSerializers
{
    public class StructBinarySerializer : IBinarySerializer
    {
        unsafe public byte[] SerializeObject<T>(T obj)
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

        unsafe public bool TryDeserializeObject<T>(byte[] bytes, out T value)
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
    }
}
#endif