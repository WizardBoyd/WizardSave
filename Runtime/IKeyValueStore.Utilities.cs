using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WizardSave
{
    public static partial class IKeyValueStoreExtensions
    {
        public static bool DoesListMatchConstructor<T>(List<object> objects)
        {
            // Step 1: Identify the Constructor
            // Assuming you're looking for a specific constructor, e.g., based on a known parameter count
            ConstructorInfo constructor = typeof(T).GetConstructors()
                .FirstOrDefault(c => c.GetParameters().Length == objects.Count);

            if (constructor == null)
            {
                Console.WriteLine("No matching constructor found for the given object count.");
                return false;
            }

            // Step 2: Get Parameter Types
            Type[] parameterTypes = constructor.GetParameters().Select(p => p.ParameterType).ToArray();

            // Step 3: Compare Parameter Types to Object Types
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == null || !parameterTypes[i].IsAssignableFrom(objects[i].GetType()))
                {
                    return false;
                }
            }

            // All checks passed
            return true;
        }
        
        public static bool DoesListMatchConstructor(Type type , List<object> objects)
        {
            // Step 1: Identify the Constructor
            // Assuming you're looking for a specific constructor, e.g., based on a known parameter count
            ConstructorInfo constructor = type.GetConstructors()
                .FirstOrDefault(c => c.GetParameters().Length == objects.Count);

            if (constructor == null)
            {
                Console.WriteLine("No matching constructor found for the given object count.");
                return false;
            }

            // Step 2: Get Parameter Types
            Type[] parameterTypes = constructor.GetParameters().Select(p => p.ParameterType).ToArray();

            // Step 3: Compare Parameter Types to Object Types
            for (int i = 0; i < objects.Count; i++)
            {
                if (objects[i] == null || !parameterTypes[i].IsAssignableFrom(objects[i].GetType()))
                {
                    return false;
                }
            }

            // All checks passed
            return true;
        }
    }
}