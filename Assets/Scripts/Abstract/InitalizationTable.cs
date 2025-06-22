using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    [System.Serializable]
    public class InitalizationTable<T> where T : ScriptableObject
    {
        public int Count => tableData.Count;

        [System.Serializable]
        public class TableEntry
        {
            public TableEntry(string keyName, T scriptableObject)
            {
                KeyName = keyName;
                ScriptableObject = scriptableObject;
            }

            public string KeyName;
            public T ScriptableObject;
        }

        [SerializeField]
        private List<TableEntry> tableData;

        public T Get(string keyName)
        {
            var entry = tableData.Find(x => x.KeyName == keyName);

            if (null == entry)
            {
                Debug.LogError($"Failed to find key \"{keyName}\"");
                return null;
            }

            return entry.ScriptableObject;
        }

        public string GetKey(int index)
        {
            return tableData[index].KeyName;
        }
    }
}