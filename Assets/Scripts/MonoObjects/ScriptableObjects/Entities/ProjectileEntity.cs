using UnityEngine;

namespace BelowSeaLevel_25
{
    public interface IProjectile
    {
        int GetDamage();
        float GetSpeed();
        float GetAliveTime();
    }

    [CreateAssetMenu(fileName = "ProjectileEntity", menuName = "Scriptable Objects/Entities/ProjectileEntity")]
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
