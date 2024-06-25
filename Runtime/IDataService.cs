using System.Collections.Generic;

namespace WizardSave
{
    public interface IDataService
    {
        void Save(GameData data, bool overwrite = true);
        T Load<T>(string fileName) where T : GameData,new();
        void Delete(string fileName);
        void DeleteAll();
        IEnumerable<string> ListSaves();
        bool FileExists(string fileName);
    }
}