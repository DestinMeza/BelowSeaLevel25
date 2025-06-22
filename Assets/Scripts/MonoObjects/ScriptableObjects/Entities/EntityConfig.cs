using UnityEngine;

namespace BelowSeaLevel_25
{
    [CreateAssetMenu(fileName = "EntityConfig", menuName = "Scriptable Objects/EntityConfig")]
    public class EntityConfig : ScriptableObject
    {
        public int MaxCannonCount => m_MaxCannonCount;
        public int MaxEnemyCount => m_MaxEnemyCount;
        public int MaxProjectileCount => m_MaxProjectileCount;

        [SerializeField]
        private int m_MaxCannonCount;

        [SerializeField]
        private int m_MaxEnemyCount;

        [SerializeField]
        private int m_MaxProjectileCount;

        public int GetMaxFromKey(string key)
        {
            if (key == "Cannon")
            {
                return MaxCannonCount;
            }
            if (key == "Enemy")
            {
                return MaxEnemyCount;
            }
            if (key == "Bomb" || key == "Lazer")
            {
                return MaxProjectileCount;
            }

            return 0;
        }
    }
}
