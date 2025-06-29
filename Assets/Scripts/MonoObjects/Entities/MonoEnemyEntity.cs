using System.Collections;
using System.Collections.Generic;
using BelowSeaLevel_25.AI;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoEnemyEntity : MonoEntity
    {
        public SpriteRenderer spriteRenderer;
        public Rigidbody2D rb2D;
        public Vector3 TargetDirection = Vector3.down;
        public Vector2 RelativeFacingDirection = Vector2.down;
        public Collider2D physicsCollider;

        public Vector2 TargetVelocity = new Vector2();

        public int currentHealth;
        public int MaxHealth => m_Health;

        private BehaviorTree m_BehaviorTree;
        private int m_Score;
        private int m_Damage;
        private int m_Health;
        private float m_Speed;
        private Sprite m_Sprite;
        private List<Sprite> m_HitSprites;
        private List<Sprite> m_AttackSprites;
        private List<Sprite> m_DeathSprites;
        private float m_AnimationSpeed;

        public int GetDamage() => m_Damage;

        public float GetSpeed() => m_Speed;

        private bool IsDead => currentHealth <= 0;
        private Coroutine m_ActiveEffectColorCoroutine;
        private Coroutine m_ActiveEffectCoroutine;
        private Color m_StartingRendererColor;

        public override void OnEnable()
        {
            currentHealth = m_Health;
            m_BehaviorTree?.Reset();
            physicsCollider.enabled = true;
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
            m_HitSprites = enemy.GetHitSprites();
            m_AttackSprites = enemy.GetAttackSprites();
            m_DeathSprites = enemy.GetDeathSprites();
            m_AnimationSpeed = enemy.GetAnimationSpeed();

            m_StartingRendererColor = spriteRenderer.color;

            switch (enemy.GetEnemyKind())
            {
                case EnemyKind.Pufferfish:
                    m_BehaviorTree = new PufferFishTree(this);
                    break;
            }

            if (m_Sprite != null)
            {
                spriteRenderer.sprite = m_Sprite;
            }

            TargetVelocity = TargetDirection * m_Speed;
        }


        public void Update()
        {
            if (IsDead)
            {
                return;
            }

            m_BehaviorTree.Process();
        }

        public void FixedUpdate()
        {
            rb2D.linearVelocity = TargetVelocity;
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
            EntityManager.Spawn<MonoEffectEntity>("HitEffect", entity.transform.position);
            entity.gameObject.SetActive(false);
            CameraManager.ShakeCamera(0.10f, 0.05f);
        }

        public void ProcessDamage(MonoLazerEntity entity)
        {
            SubHealth(entity.GetDamage());
            EntityManager.Spawn<MonoEffectEntity>("HitEffect", transform.position);
        }

        public void SubHealth(int damage)
        {
            currentHealth -= damage;

            if (IsDead)
            {
                GameManager.Player.AddScore(m_Score);

                if (m_ActiveEffectCoroutine != null)
                {
                    StopCoroutine(m_ActiveEffectCoroutine);
                }

                m_ActiveEffectCoroutine = StartCoroutine(PlayDeathEffect());
                TargetVelocity = Vector2.zero;
                physicsCollider.enabled = false;
            }
            else
            {
                if (m_ActiveEffectCoroutine != null)
                {
                    StopCoroutine(m_ActiveEffectCoroutine);
                }

                m_ActiveEffectCoroutine = StartCoroutine(PlayHitEffect());

                if (m_ActiveEffectColorCoroutine != null)
                {
                    StopCoroutine(m_ActiveEffectColorCoroutine);
                    spriteRenderer.color = m_StartingRendererColor;
                }
                
                m_ActiveEffectColorCoroutine = StartCoroutine(PlayColorHitEffect());
            }
        }

        public void StartAttackEffect()
        { 
           m_ActiveEffectCoroutine = StartCoroutine(PlayAttackEffect());
        }

        private IEnumerator PlayColorHitEffect()
        {
            yield return new WaitForEndOfFrame();

            Color black = new Color(0, 0, 0, 1);
            float duration = 1.0f;
            for (float t = 0; t < duration; t += Time.deltaTime)
            {
                spriteRenderer.color = Color.Lerp(black, m_StartingRendererColor, t / duration);
            }

            spriteRenderer.color = m_StartingRendererColor;
        }

        private IEnumerator PlayHitEffect()
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < m_HitSprites.Count; i++)
            {
                spriteRenderer.sprite = m_HitSprites[i];
                yield return new WaitForSeconds(m_AnimationSpeed);
            }

            m_ActiveEffectCoroutine = null;
            spriteRenderer.sprite = m_Sprite;
        }

        private IEnumerator PlayAttackEffect()
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < m_AttackSprites.Count; i++)
            {
                spriteRenderer.sprite = m_AttackSprites[i];
                yield return new WaitForSeconds(m_AnimationSpeed);
            }

            m_ActiveEffectCoroutine = null;
            spriteRenderer.sprite = m_Sprite;
        }

        private IEnumerator PlayDeathEffect()
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < m_DeathSprites.Count; i++)
            {
                spriteRenderer.sprite = m_DeathSprites[i];
                yield return new WaitForSeconds(m_AnimationSpeed);
            }

            m_ActiveEffectCoroutine = null;
            spriteRenderer.sprite = m_Sprite;
            gameObject.SetActive(false);
        }
    }
}
