using UnityEngine;

namespace BelowSeaLevel_25
{
    public interface IEnemy
    {
        public int GetScore();
        public int GetDamage();
        public int GetHealth();
        public float GetSpeed();
        public Sprite GetSprite();
    }

    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Entities/Enemy")]
    public class EnemyEntity : Entity, IEnemy
    {
        [SerializeField] private string Name;
        [SerializeField] private Sprite EnemySprite;
        [SerializeField] private int Score;
        [SerializeField] private int Health;
        [SerializeField] private int Damage;
        [SerializeField] private float Speed;

        public string GetName() => Name;
        public Sprite GetSprite() => EnemySprite;
        public int GetScore() => Score;
        public int GetDamage() => Damage;
        public int GetHealth() => Health;
        public float GetSpeed() => Speed;
    }
}
