using UnityEngine;
using static BelowSeaLevel_25.Globals;

namespace BelowSeaLevel_25
{
    [CreateAssetMenu(fileName = "LazerCard", menuName = "Scriptable Objects/LazerCard")]
    public class LazerCard : Card
    {
        public override void OnActivate()
        {
            Transform target = GameState.ActivePlayer.FiringPoint.transform;
            Transform transform = GameState.ActivePlayer.CannonPivot.transform;

            GameState.ActivePlayer.PlayCannonEffect();
            GameState.ActivePlayer.PlayFiringEffectLazer();

            MonoLazerEntity lazerEntity = EntityManager.Spawn<MonoLazerEntity>(
                key: "Lazer",
                targetPosition: target.position,
                upAngleDegrees: 0,
                onSpawnEvent: 
                delegate (MonoEntity x)
                { 
                    MonoLazerEntity lazerEntity = x as MonoLazerEntity;
                    lazerEntity.TargetDirection = target.up;
                } 
            );
        }
    }
}
