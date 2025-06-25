using UnityEngine;

namespace BelowSeaLevel_25
{
    [CreateAssetMenu(fileName = "Cannon", menuName = "Scriptable Objects/Entities/Cannon")]
    public class CannonEntity : Entity
    {
        [SerializeField] private Sprite BarrelBase;
        [SerializeField] private Sprite BarrelSprite;


        public Sprite GetBarrelBase() => BarrelBase;

        public Sprite GetBarrel() => BarrelSprite;
    }
}