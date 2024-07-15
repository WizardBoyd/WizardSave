using System.IO;

namespace WizardSave
{
    public interface IStreamSavable
    {
        void Load(Stream stream);
        void Save(Stream stream);
    }
    
    public interface IStreamSavableKeyValueStore : IKeyValueStore, IStreamSavable{}
}