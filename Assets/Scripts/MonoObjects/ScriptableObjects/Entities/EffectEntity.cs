using UnityEngine;
using System.Collections.Generic;

namespace BelowSeaLevel_25
{
    public interface IEffect
    {
        public List<Sprite> GetSprites();
        public float GetAnimationSpeed();
    }

    [CreateAssetMenu(fileName = "Effect", menuName = "Scriptable Objects/Entities/Effect")]
    public class EffectEntity : Entity, IEffect
    {
        [SerializeField] private List<Sprite> Sprites;
        [SerializeField] private float AnimationSpeed;

        public List<Sprite> GetSprites() => Sprites;
        public float GetAnimationSpeed() => AnimationSpeed;
    }
}