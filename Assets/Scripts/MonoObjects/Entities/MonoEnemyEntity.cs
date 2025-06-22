using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoEnemyEntity : MonoEntity
    {
        public Rigidbody2D rb2D;
        public Vector3 TargetDirection = Vector3.down;

        public int currentHealth;
        public int MaxHealth => m_Health;

        private int m_Score;
        private int m_Damage;
        private int m_Health;
        private float m_Speed;

        public int GetDamage() => m_Damage;

        public override void OnEnable()
        {
            currentHealth = m_Health;
        }

        public override void Init(Entity entity)
        {
            base.Init(entity);

            IEnemy enemy = Entity as IEnemy;
            m_Speed = enemy.GetSpeed();
            m_Health = enemy.GetHealth();
            m_Damage = enemy.GetDamage();
            m_Score = enemy.GetScore();
        }

        public void FixedUpdate()
        {
            IEnemy enemy = Entity as IEnemy;

            rb2D.linearVelocity = TargetDirection * m_Speed;
            transform.up = rb2D.linearVelocity.normalized;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var entity = collision.gameObject.GetComponentInParent<MonoProjectileEntity>();

            if (entity != null)
            {
                SubHealth(entity.GetDamage());
                entity.gameObject.SetActive(false);
            }
        }

        public void SubHealth(int damage)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                GameManager.Player.AddScore(m_Score);
                gameObject.SetActive(false);
            }
        }
    }
}
