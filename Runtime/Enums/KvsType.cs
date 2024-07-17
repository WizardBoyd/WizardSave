namespace WizardSave.Enums
{
    public enum KvsType
    {
        // Memroy is the default key value store type
        Memory = 1,
        // CentralKvs is the key value store type that there is only one instance in the application aka like playerprefs or android keystore
        CentralKvs = 2,
        // File is the key value store type that the data is saved in a file
        File = 3,
        // Database is the key value store type that the data is saved in a database
        Database = 4,
    }
}