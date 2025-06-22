using UnityEngine;

namespace BelowSeaLevel_25
{
    public interface IProjectile
    {
        int GetDamage();
        float GetSpeed();
    }

    [CreateAssetMenu(fileName = "ProjectileEntity", menuName = "Scriptable Objects/Entities/ProjectileEntity")]
    public class ProjectileEntity : Entity, IProjectile
    {
        public string Name;
        public Sprite ProjectileSprite;
        public int Damage;
        public float Speed;

        public int GetDamage() => Damage;
        public float GetSpeed() => Speed;
    }
}
