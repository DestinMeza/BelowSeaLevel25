using System.Collections;
using UnityEngine;
using static BelowSeaLevel_25.Globals;

namespace BelowSeaLevel_25
{
    [CreateAssetMenu(fileName = "BombCard", menuName = "Scriptable Objects/BombCard")]
    public class BombCard : Card
    {
        public override void OnActivate()
        {
            Transform target = GameState.ActivePlayer.FiringPoint.transform;
            Transform transform = GameState.ActivePlayer.CannonPivot.transform;

            GameState.ActivePlayer.PlayCannonEffect();
            GameState.ActivePlayer.PlayFiringEffect();
            AudioManager.PlaySFXClip("BombLaunch");

            float upAngle = Vector3.Angle(transform.up, target.up);
            MonoProjectileEntity projectileEntity = EntityManager.Spawn<MonoProjectileEntity>(
                key: "Bomb",
                targetPosition: target.position,
                upAngleDegrees: upAngle,
                onSpawnEvent: 
                delegate (MonoEntity x)
                {
                    MonoProjectileEntity bomb = x as MonoProjectileEntity;
                    bomb.TargetDirection = transform.up;
                    bomb.OnDeathCallback = delegate ()
                    {
                        Vector3 bombDeathPosition = bomb.transform.position;
                        AudioManager.PlaySFXClip("BombExplode");

                        MonoExplosionEntity bombExplosionEntity = EntityManager.Spawn<MonoExplosionEntity>(
                                       key: "BombExplosion",
                                       targetPosition: bombDeathPosition
                        );
                    };
                }
            );
        }
    }
}
