using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoExplosionEntity : MonoEntity
    {
        public SpriteRenderer spriteRenderer;

        private List<Sprite> m_Sprites;
        private float m_AnimationSpeed;
        private float m_Radius;
        private int m_Damage;
        private int EnemyLayer => LayerMask.GetMask("Enemy");

        public override void OnEnable()
        {
            if (m_Sprites == null) return;

            StartCoroutine(PlayExplosion());
        }

        public override void Init(Entity entity)
        {
            base.Init(entity);

            var explosion = entity as IExplosion;
            m_Sprites = explosion.GetSprites();
            m_AnimationSpeed = explosion.GetAnimationSpeed();
            m_Radius = explosion.GetRadius();
            m_Damage = explosion.GetDamage();

            spriteRenderer.transform.localScale = new Vector3(m_Radius * 0.5f, m_Radius * 0.5f, 1.0f);

            StartCoroutine(PlayExplosion());
        }

        private IEnumerator PlayExplosion()
        {
            CameraManager.ShakeCamera(0.33f, 0.25f);
            yield return new WaitForEndOfFrame();

            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, m_Radius, EnemyLayer);

            var enemies = colliders
                .Where(x => x.GetComponentInParent<MonoEnemyEntity>())
                .Select(x => x.GetComponentInParent<MonoEnemyEntity>())
                .ToList();

            Debug.Log($"Hit Enemies: {enemies.Count}");

            for (int i = 0; i < enemies.Count; i++)
            {
                enemies[i].SubHealth(m_Damage);
            }

            for (int i = 0; i < m_Sprites.Count; i++)
            {
                spriteRenderer.sprite = m_Sprites[i];
                yield return new WaitForSeconds(m_AnimationSpeed);
                yield return new WaitForEndOfFrame();
            }

            spriteRenderer.sprite = m_Sprites[0];
            gameObject.SetActive(false);
        }
    }
}
