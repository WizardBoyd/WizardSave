using System;
using UnityEngine;

namespace WizardSave.ObjectSerializers
{
    public class JsonUtilityTextSerializer : ITextSerializer
    {
        public bool PrettyPrint {get; set;}
        
        public string SerializeObject<T>(T obj)
        {
            return JsonUtility.ToJson(obj, PrettyPrint);
        }

        public bool TryDeserializeObject<T>(string data, out T obj)
        {
            try
            {
                obj = JsonUtility.FromJson<T>(data);
                return true;
            }
            catch(Exception e)
            {
                obj = default;
                return false;
            }
        }
    }
}