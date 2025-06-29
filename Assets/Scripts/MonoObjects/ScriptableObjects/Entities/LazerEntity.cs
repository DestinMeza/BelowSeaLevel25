using UnityEngine;

namespace BelowSeaLevel_25
{
    public interface ILazer
    {
        int GetDamage();

        float GetAliveTime();
    }

    [CreateAssetMenu(fileName = "Lazer", menuName = "Scriptable Objects/Entities/Lazer")]
    public class LazerEntity : Entity, ILazer
    {
        [SerializeField] private string Name;
        [SerializeField] private Sprite LazerSprite;
        [SerializeField] private int Damage;
        [SerializeField] private float AliveTime;
        
        public string GetName() => Name;
        public Sprite GetLazerSprite() => LazerSprite;
        public int GetDamage() => Damage;
        public float GetAliveTime() => AliveTime;
    }
}
