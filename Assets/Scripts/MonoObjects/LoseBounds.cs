using UnityEngine;

namespace BelowSeaLevel_25
{
    public class LoseBounds : MonoBehaviour
    {
        public void OnCollisionEnter2D(Collision2D collision)
        {
            var enemy = collision.collider.GetComponentInParent<MonoEnemyEntity>();
            if (enemy == null)
            {
                return;
            }

            GameManager.Player.SubHealth(enemy.GetDamage());
        }
    }
}
