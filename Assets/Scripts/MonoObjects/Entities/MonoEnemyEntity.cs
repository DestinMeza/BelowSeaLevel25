using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoEnemyEntity : MonoEntity
    {
        public Rigidbody2D rb2D;
        public Vector3 TargetDirection = Vector3.down;

        public void FixedUpdate()
        {
            IEnemy enemy = Entity as IEnemy;

            rb2D.linearVelocity = TargetDirection * enemy.GetSpeed();
        }
    }
}
