using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoProjectileEntity : MonoEntity
    {
        public Rigidbody2D rb2D;
        public Vector3 TargetDirection;

        private int m_Damage;
        private float m_Speed;

        public override void Init(Entity entity)
        {
            base.Init(entity);

            IProjectile projectile = Entity as IProjectile;
            m_Damage = projectile.GetDamage();
            m_Speed = projectile.GetSpeed();
        }

        public void FixedUpdate()
        {
            rb2D.linearVelocity = TargetDirection * m_Speed;
            transform.up = rb2D.linearVelocity.normalized;
        }

        public int GetDamage() => m_Damage;
    }
}
