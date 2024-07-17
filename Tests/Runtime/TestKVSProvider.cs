using System;
using System.IO;
using UnityEngine;
using WizardSave.Attributes;

namespace WizardSave.Tests.Runtime
{
    public class TestKVSProvider : MonoBehaviour
    {
        private DictionaryKeyValueStore _dictionaryKeyValueStore;
        
        private void Awake()
        {
            _dictionaryKeyValueStore = new DictionaryKeyValueStore()
            {
                FilePath = Path.Combine(Application.persistentDataPath, "testKey.data")
            };
            _dictionaryKeyValueStore.Load();
        }
        

        [KvsSource(typeof(DictionaryKeyValueStore), "testKey")]
        public DictionaryKeyValueStore GetDictionaryKeyValueStore()
        {
            return _dictionaryKeyValueStore;
        }

        private void OnDestroy()
        {
            _dictionaryKeyValueStore.Save();
        }
    }
}