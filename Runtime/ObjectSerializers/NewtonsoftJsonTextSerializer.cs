using System;
using Newtonsoft.Json;

namespace WizardSave.ObjectSerializers
{
    public class NewtonsoftJsonTextSerializer : ITextSerializer
    {
        public JsonSerializerSettings Settings { get; set; }

        public NewtonsoftJsonTextSerializer()
        {
            Settings = new JsonSerializerSettings();
        }
        
        public string SerializeObject<T>(T obj)
        {
           return JsonConvert.SerializeObject(obj, Settings);
        }

        public string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, Settings);
        }

        public bool TryDeserializeObject<T>(string data, out T obj)
        {
            try
            {
                obj = JsonConvert.DeserializeObject<T>(data, Settings);
                return true;
            }
            catch (Exception)
            {
                obj = default;
                return false;
            }
        }

        public bool TryDeserializeObject(string data, Type type, out object obj)
        {
            try
            {
                obj = JsonConvert.DeserializeObject(data, type, Settings);
                return true;
            }
            catch (Exception)
            {
                obj = default;
                return false;
            }
        }
    }
}