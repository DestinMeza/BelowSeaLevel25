using System;
using System.Collections.Generic;
using BelowSeaLevel_25.AI;
using UnityEngine;

namespace BelowSeaLevel_25
{
    [Serializable]
    public enum EnemyKind
    {
        Pufferfish
    }
    
    public interface IEnemy
    {
        public EnemyKind GetEnemyKind();
        public int GetScore();
        public int GetDamage();
        public int GetHealth();
        public float GetSpeed();
        public float GetAnimationSpeed();
        public List<Sprite> GetHitSprites();
        public List<Sprite> GetAttackSprites();
        public List<Sprite> GetDeathSprites();
        public Sprite GetSprite();
    }

    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Entities/Enemy")]
    public class EnemyEntity : Entity, IEnemy
    {
        [SerializeField] private string Name;
        [SerializeField] private EnemyKind EnemyKind;
        [SerializeField] private Sprite EnemySprite;
        [SerializeField] private int Score;
        [SerializeField] private int Health;
        [SerializeField] private int Damage;
        [SerializeField] private float Speed;
        [SerializeField] private float AnimationSpeed;
        [SerializeField] private List<Sprite> HitSprites;
        [SerializeField] private List<Sprite> AttackSprites;
        [SerializeField] private List<Sprite> DeathSprites;

        public string GetName() => Name;
        public EnemyKind GetEnemyKind() => EnemyKind;
        public Sprite GetSprite() => EnemySprite;
        public int GetScore() => Score;
        public int GetDamage() => Damage;
        public int GetHealth() => Health;
        public float GetSpeed() => Speed;
        public float GetAnimationSpeed() => AnimationSpeed;
        public List<Sprite> GetHitSprites() => HitSprites;
        public List<Sprite> GetAttackSprites() => AttackSprites;
        public List<Sprite> GetDeathSprites() => DeathSprites;
    }
}
