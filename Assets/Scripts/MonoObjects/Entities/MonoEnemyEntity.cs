using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoEnemyEntity : MonoEntity
    {
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rb2D;
        public Vector3 TargetDirection = Vector3.down;
        public Vector2 RelativeFacingDirection = Vector2.down;

        public Vector2 TargetVelocity = new Vector2();

        public int currentHealth;
        public int MaxHealth => m_Health;

        private int m_Score;
        private int m_Damage;
        private int m_Health;
        private float m_Speed;
        private Sprite m_Sprite;

        public int GetDamage() => m_Damage;

        public float GetSpeed() => m_Speed;

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
            m_Sprite = enemy.GetSprite();

            if (m_Sprite != null)
            {
                spriteRenderer.sprite = m_Sprite;
            }

            TargetVelocity = TargetDirection * m_Speed;
        }

        public void Update()
        { 

        }

        public void FixedUpdate()
        {
            rb2D.linearVelocity = TargetDirection * m_Speed;
            transform.up = RelativeFacingDirection;
        }

        public void SetTargetVelocity(Vector2 targetVelocity)
        {
            TargetVelocity = targetVelocity;
        }

        public void SetRelativeFacingDirection(Vector2 relativeFacingDir)
        {
            RelativeFacingDirection = relativeFacingDir;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            var entity = collision.gameObject.GetComponentInParent<MonoProjectileEntity>();

            if (entity != null)
            {
                ProcessDamage(entity);
            }
        }

        public void ProcessDamage(MonoProjectileEntity entity)
        { 
            SubHealth(entity.GetDamage());
            entity.gameObject.SetActive(false);
        }

        public void ProcessDamage(MonoLazerEntity entity)
        { 
            SubHealth(entity.GetDamage());
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
