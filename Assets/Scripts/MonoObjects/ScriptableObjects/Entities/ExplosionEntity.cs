using UnityEngine;
using System.Collections.Generic;


namespace BelowSeaLevel_25
{
    public interface IExplosion
    {
        public List<Sprite> GetSprites();
        public float GetAnimationSpeed();
        public float GetRadius();
        public int GetDamage();
    }

    [CreateAssetMenu(fileName = "Explosion", menuName = "Scriptable Objects/Entities/Explosion")]
    public class ExplosionEntity : Entity, IExplosion
    {
        [SerializeField] private List<Sprite> Sprites;
        [SerializeField] private float AnimationSpeed;
        [SerializeField] private float Radius;
        [SerializeField] private int Damage;

        public List<Sprite> GetSprites() => Sprites;
        public float GetAnimationSpeed() => AnimationSpeed;
        public float GetRadius() => Radius;
        public int GetDamage() => Damage;
    }
}