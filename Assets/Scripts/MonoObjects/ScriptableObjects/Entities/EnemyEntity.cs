using UnityEngine;

namespace BelowSeaLevel_25
{
    public interface IEnemy
    {
        public int GetScore();
        public int GetDamage();
        public int GetHealth();
        public float GetSpeed();
    }

    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Entities/Enemy")]
    public class EnemyEntity : Entity, IEnemy
    {
        public string Name;
        public Sprite EnemySprite;
        public int Score;
        public int Health;
        public int Damage;
        public float Speed;

        public int GetScore() => Score;
        public int GetDamage() => Damage;
        public int GetHealth() => Health;
        public float GetSpeed() => Speed;
    }
}
