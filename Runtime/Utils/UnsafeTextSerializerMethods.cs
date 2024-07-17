using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Unity.Collections.LowLevel.Unsafe;

namespace WizardSave.Utils
{
    public static class UnsafeTextSerializerMethods
    {
        public const char NumberSeparator = ',';
        
        unsafe public static string SerializeInts<T>(T value) where T : struct
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
        
        unsafe public static bool TryDeserializeInts<T>(string text, out T value) where T : struct
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

        unsafe public static string SerializeFloats<T>(T value) where T : struct
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

        unsafe public static bool TryDeserializeFloats<T>(string text, out T value) where T : struct
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
    }
}