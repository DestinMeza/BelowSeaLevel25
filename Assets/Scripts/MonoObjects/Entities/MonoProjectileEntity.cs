using System.Collections;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoProjectileEntity : MonoEntity
    {
        public delegate void OnDeath();

        public OnDeath OnDeathCallback = delegate { };

        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rb2D;
        public Vector3 TargetDirection;
        private float lastEnabledTime = 0;
        private float m_AliveTime;
        private int m_Damage;
        private float m_Speed;
        private Sprite m_ProjectileSprite;

        public override void Init(Entity entity)
        {
            base.Init(entity);

            IProjectile projectile = Entity as IProjectile;
            m_AliveTime = projectile.GetAliveTime();
            m_Damage = projectile.GetDamage();
            m_Speed = projectile.GetSpeed();
            m_ProjectileSprite = projectile.GetProjectileSprite();

            spriteRenderer.sprite = m_ProjectileSprite;
        }

        public void Update()
        {
            if (Time.time - lastEnabledTime > m_AliveTime)
            {
                gameObject.SetActive(false);
            }
        }

        public void FixedUpdate()
        {
            rb2D.linearVelocity = TargetDirection * m_Speed;
            transform.up = rb2D.linearVelocity.normalized;
        }

        public override void OnEnable()
        {
            lastEnabledTime = Time.time;
        }

        public override void OnDisable()
        {
            OnDeathCallback();
        }

        public int GetDamage() => m_Damage;
    }
}
