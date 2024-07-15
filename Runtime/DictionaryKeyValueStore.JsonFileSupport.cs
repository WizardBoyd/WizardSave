// #if HAVE_NEWTONSOFT_JSON

using System.Collections.Generic;
using System.IO;
using WizardSave.Utils;
using Newtonsoft.Json;

namespace WizardSave
{
    public partial class DictionaryKeyValueStore : AStreamSavableFile, ISaveableKeyValueStore, IStreamSavableKeyValueStore, IFileKeyValueStore
    {
        public readonly Newtonsoft.Json.JsonSerializer JsonSerializer = Newtonsoft.Json.JsonSerializer.CreateDefault();

        public override void Load(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                _dictionary = (Dictionary<string, object>) JsonSerializer.Deserialize(streamReader, typeof(Dictionary<string, object>));
            }
        }

        public override void Save(Stream stream)
        {
            using (var streamWriter = new StreamWriter(stream))
            {
                JsonSerializer.Serialize(streamWriter, _dictionary);
            }
        }
    }
}
// #endif