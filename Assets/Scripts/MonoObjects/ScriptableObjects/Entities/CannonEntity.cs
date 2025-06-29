using UnityEngine;
using System.Collections.Generic;

namespace BelowSeaLevel_25
{
    [CreateAssetMenu(fileName = "Cannon", menuName = "Scriptable Objects/Entities/Cannon")]
    public class CannonEntity : Entity
    {
        [SerializeField] private Sprite BarrelBase;
        [SerializeField] private Sprite BarrelSprite;
        [SerializeField] private float AnimationSpeed;
        [SerializeField] private List<Sprite> CannonFiringSprites;
        [SerializeField] private List<Sprite> FireEffectSprites;
        [SerializeField] private List<Sprite> LazerEffectSprites;

        public Sprite GetBarrelBase() => BarrelBase;
        public Sprite GetBarrel() => BarrelSprite;
        public float GetAnimationSpeed() => AnimationSpeed;
        public List<Sprite> GetCannonFiringSprites() => CannonFiringSprites;
        public List<Sprite> GetFireEffectSprites() => FireEffectSprites;
        public List<Sprite> GetLazerEffectSprites() => LazerEffectSprites;

    }
}