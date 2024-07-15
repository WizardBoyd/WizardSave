using System;
using System.Threading.Tasks;
using UnityEngine;
using WizardSave.Utils;

namespace WizardSave
{
    public class AutoSaveKeyValueStoreWrapper : AKeyValueStoreWrapper<ISaveableKeyValueStore>, ISaveableKeyValueStore
    {
        private bool m_isSaveScheduled = false;

        public AutoSaveKeyValueStoreWrapper(ISaveableKeyValueStore kvs) : base(kvs)
        {
        }

        public void Load()
        {
            WrappedKeyValueStore.Load();
        }

        public void Save()
        {
            WrappedKeyValueStore.Save();
        }

        public override void DeleteKey(string key)
        {
            base.DeleteKey(key);
            SaveNextFrame();
        }

        public override void DeleteAll()
        {
            base.DeleteAll();
            SaveNextFrame();
        }

        public override void SetBool(string key, bool value)
        {
            base.SetBool(key, value);
            SaveNextFrame();
        }

        public override void SetInt(string key, int value)
        {
            base.SetInt(key, value);
            SaveNextFrame();
        }

        public override void SetLong(string key, long value)
        {
            base.SetLong(key, value);
            SaveNextFrame();
        }

        public override void SetFloat(string key, float value)
        {
            base.SetFloat(key, value);
            SaveNextFrame();
        }

        public override void SetDouble(string key, double value)
        {
            base.SetDouble(key, value);
            SaveNextFrame();
        }

        public override void SetString(string key, string value)
        {
            base.SetString(key, value);
            SaveNextFrame();
        }

        public override void SetBytes(string key, byte[] value)
        {
            base.SetBytes(key, value);
            SaveNextFrame();
        }


        private async void SaveNextFrame()
        {
            if (m_isSaveScheduled)
            {
                return;
            }
            m_isSaveScheduled = true;
            try
            {
                await Task.Yield();
                WrappedKeyValueStore.Save();
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
            finally
            {
                m_isSaveScheduled = false;
            }
        }
    }
}