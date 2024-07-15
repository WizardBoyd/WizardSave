using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace WizardSave.ObjectSerializers
{
    public class XmlTextSerializer : ITextSerializer
    {
        private readonly static Dictionary<Type, XmlSerializer> m_xmlSerializerCache = new Dictionary<Type, XmlSerializer>();

        public XmlSerializer GetCachedXmlSerializer(Type type)
        {
            if (!m_xmlSerializerCache.TryGetValue(type, out var serializer))
            {
                serializer = new XmlSerializer(type);
                m_xmlSerializerCache[type] = serializer;
            }
            return serializer;
        }
        
        public bool TryDeserializeObject<T>(string data, out T obj)
        {
            try
            {
                var serializer = GetCachedXmlSerializer(typeof(T));
                obj = (T)serializer.Deserialize(new System.IO.StringReader(data));
                return true;
            }
            catch(Exception e)
            {
                obj = default;
                return false;
            }
        }
        
        public string SerializeObject<T>(T obj)
        {
            var serializer = GetCachedXmlSerializer(typeof(T));
            using (var writer = new System.IO.StringWriter())
            {
                serializer.Serialize(writer, obj);
                return writer.ToString();
            }
        }
    }
}