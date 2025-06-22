using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoProjectileEntity : MonoEntity
    {
        public Rigidbody2D rb2D;
        public Vector3 TargetDirection;

        public void FixedUpdate()
        {
            IProjectile projectile = Entity as IProjectile;

            rb2D.linearVelocity = TargetDirection * projectile.GetSpeed();
            transform.up = rb2D.linearVelocity.normalized;
        }
    }
}
