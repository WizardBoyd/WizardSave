namespace WizardSave
{
    public interface ISaveable
    {
        void Load();
        void Save();
    }
    
    public interface ISaveableKeyValueStore : IKeyValueStore, ISaveable{}
}