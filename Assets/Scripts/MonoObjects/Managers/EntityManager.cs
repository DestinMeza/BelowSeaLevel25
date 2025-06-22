using System.Collections.Generic;
using System.Linq;
using UnityEditor.EditorTools;
using UnityEngine;

namespace BelowSeaLevel_25
{
    internal class EntityManager : MonoManager<EntityManager>
    {
        public EntityConfig entityConfig;
        public InitalizationTable<Entity> entityTable;

        [Tooltip("Ensure mapping is the same as Entity Table")]
        public MonoEntity[] prefabs; 
        private Dictionary<string, List<MonoEntity>> m_AllEntities;
        private Dictionary<string, MonoEntity> m_EntityPrefabMap;

        public override void Init()
        {
            base.Init();

            m_AllEntities = new();
            m_EntityPrefabMap = new();

            for (int i = 0; i < entityTable.Count; i++)
            {
                string key = entityTable.GetKey(i);
                m_EntityPrefabMap.Add(key, prefabs[i]);

                Entity entity = entityTable.Get(key);
                int count = entityConfig.GetMaxFromKey(key);

                List<MonoEntity> monoEntities = new();
                for (int j = 0; j < count; j++)
                {
                    MonoEntity monoEntity = GameObject.Instantiate(m_EntityPrefabMap[key], new Vector3(10, 0, 0), Quaternion.identity);
                    monoEntity.Init(entity);
                    monoEntity.gameObject.SetActive(false);
                    monoEntities.Add(monoEntity);
                }

                m_AllEntities.Add(key, monoEntities);
            }
        }

        public static MonoEntity Spawn(string key, Vector3 targetPosition, float upAngleDegrees = 0)
        {
            if (!Instance.m_AllEntities.TryGetValue(key, out List<MonoEntity> entities))
            {
                Debug.LogError($"Failed spawning object with key {key}");
                return null;
            }

            Debug.Log($"Spawning {key}...");

            MonoEntity monoEntity = entities.Find(x => !x.isActiveAndEnabled);

            if (null == monoEntity)
            {
                monoEntity = GameObject.Instantiate(Instance.m_EntityPrefabMap[key], targetPosition, Quaternion.identity);
                Entity entity = Instance.entityTable.Get(key);

                monoEntity.Init(entity);
                entities.Add(monoEntity);
            }

            float angleInRads = Mathf.Deg2Rad * upAngleDegrees;

            monoEntity.transform.position = targetPosition;
            monoEntity.transform.rotation = Quaternion.AngleAxis(angleInRads, Vector3.forward);
            monoEntity.gameObject.SetActive(true);

            return monoEntity;
        }
    }
}