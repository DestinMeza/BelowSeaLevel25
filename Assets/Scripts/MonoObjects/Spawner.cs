using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BelowSeaLevel_25
{

    [System.Serializable]
    public class SpawnEntry
    {
        public string EnemyName;
        public float SpawnIntervalMin;
        public float SpawnIntervalMax;
    }

    public class Spawner : MonoBehaviour
    {
        public float spawnScale = 1.0f;
        public float spawnAccumulator = 0.2f;
        public List<SpawnEntry> spawnEntries;
        private SpawnEntry m_ActiveSpawnEntry;
        private int m_CurrentIndex;

        private float lastSpawnTime;
        private float m_CurrentSpawnScale;
        private Vector2 m_SpawnPosition;
        private Coroutine m_SpawnLoop;

        public void Init()
        {
            float spawnPointBounds = transform.localScale.x * 0.5f;
            m_ActiveSpawnEntry = spawnEntries.First();
            m_SpawnPosition = new Vector2(spawnPointBounds * -1, spawnPointBounds);

            m_CurrentSpawnScale = Mathf.Min(1.0f, spawnScale);

            m_SpawnLoop = StartCoroutine("SpawnLoop");
        }

        private IEnumerator SpawnLoop()
        {
            while (Globals.GameState.IsPlaying)
            {
                float waitTime = Random.Range(m_ActiveSpawnEntry.SpawnIntervalMin / m_CurrentSpawnScale, m_ActiveSpawnEntry.SpawnIntervalMax / m_CurrentSpawnScale);
                yield return new WaitForSeconds(waitTime);

                Spawn();
            }
        }

        private void Spawn()
        {
            string enemyName = m_ActiveSpawnEntry.EnemyName;
            float spawnPositionX = Random.Range(m_SpawnPosition.x, m_SpawnPosition.y);
            Vector3 spawnPosition = new Vector3(spawnPositionX, transform.position.y, 0);

            EntityManager.Spawn<MonoEnemyEntity>(enemyName, spawnPosition);
            lastSpawnTime = Time.time;

            NextSpawnEntry();
        }

        private void NextSpawnEntry()
        {
            m_CurrentIndex++;
            if (m_CurrentIndex > spawnEntries.Count - 1)
            {
                m_CurrentIndex = 0;

                m_CurrentSpawnScale = Mathf.Max(1.0f, m_CurrentSpawnScale + spawnAccumulator);
            }

            SetActiveSpawnEntry(spawnEntries[m_CurrentIndex]);
        }

        private void SetActiveSpawnEntry(SpawnEntry spawnEntry)
        {
            m_ActiveSpawnEntry = spawnEntry;
        }
    }

}