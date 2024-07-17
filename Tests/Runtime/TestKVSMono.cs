using System;
using UnityEngine;
using WizardSave.Attributes;
using WizardSave.Enums;
using WizardSave.ObjectSerializers;

namespace WizardSave.Tests.Runtime
{
    public class TestKVSMono : MonoBehaviour
    {
       [KvsProperty(typeof(DictionaryKeyValueStore), "testKey", "TestClass")]
       private TestClass testClass = new TestClass();

       // [KvsProperty(typeof(DictionaryKeyValueStore), "testKey", "TestStruct")]
       // private TestStruct testStruct = new TestStruct()
       // {
       //     TestStructInt = 187
       // };

        private void Awake()
        {
            testClass.TestClassInt = 187;
        }
    }
}