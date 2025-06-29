using UnityEngine;

namespace BelowSeaLevel_25
{
    public interface IProjectile
    {
        public int GetDamage();
        public float GetSpeed();
        public float GetAliveTime();
        public Sprite GetProjectileSprite();
    }

    [CreateAssetMenu(fileName = "Projectile", menuName = "Scriptable Objects/Entities/Projectile")]
    public class ProjectileEntity : Entity, IProjectile
    {
        [SerializeField] private string Name;
        [SerializeField] private Sprite ProjectileSprite;
        [SerializeField] private int Damage;
        [SerializeField] private float Speed;
        [SerializeField] private float AliveTime;

        public string GetName() => Name;
        public Sprite GetProjectileSprite() => ProjectileSprite;
        public int GetDamage() => Damage;
        public float GetSpeed() => Speed;
        public float GetAliveTime() => AliveTime;
    }
}
