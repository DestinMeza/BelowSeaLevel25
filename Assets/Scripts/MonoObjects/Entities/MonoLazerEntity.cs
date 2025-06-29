using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoLazerEntity : MonoEntity
    {
        public LineRenderer lineRenderer;
        public Vector3 TargetDirection;
        public delegate void OnDeath();
        public OnDeath OnDeathCallback = delegate { };

        private int m_Damage;

        private int EnemyLayer => LayerMask.GetMask("Enemy");

        private float lastEnabledTime = 0;
        private float m_lazerDuration = 2;

        private const int ORIGIN_INDEX = 0;
        private const int ENDING_INDEX = 1;

        public override void Init(Entity entity)
        {
            base.Init(entity);

            ILazer lazer = Entity as ILazer;
            m_Damage = lazer.GetDamage();
            m_lazerDuration = lazer.GetAliveTime();

        }

        public override void OnEnable()
        {
            lastEnabledTime = Time.time;
            float lazerLength = 20;

            Ray ray = new Ray(transform.position, TargetDirection);
            RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction, lazerLength, EnemyLayer);

            lineRenderer.SetPosition(ORIGIN_INDEX, ray.origin);
            lineRenderer.SetPosition(ENDING_INDEX, ray.GetPoint(lazerLength));

            var enemies = hits
                .Where(x => x.collider.GetComponentInParent<MonoEnemyEntity>())
                .Select(x => x.collider.GetComponentInParent<MonoEnemyEntity>())
                .ToList();

            Debug.Log($"Hit Enemies: {enemies.Count}");

            if (enemies.Count > 0)
            { 
                CameraManager.ShakeCamera(0.25f, 0.05f);
            }

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].ProcessDamage(this);
            }
        }

        public void Update()
        {
            if (Time.time - lastEnabledTime > m_lazerDuration)
            {
                gameObject.SetActive(false);
            }
        }

        public override void OnDisable()
        {
            OnDeathCallback();
        }

        public int GetDamage() => m_Damage;
    }
}