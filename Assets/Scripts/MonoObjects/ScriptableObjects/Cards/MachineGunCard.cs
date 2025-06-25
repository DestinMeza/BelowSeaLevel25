using UnityEngine;
using static BelowSeaLevel_25.Globals;

namespace BelowSeaLevel_25
{
    [CreateAssetMenu(fileName = "MachineGunCard", menuName = "Scriptable Objects/MachineGunCard")]
    public class MachineGunCard : Card
    {
        public override void OnActivate()
        {
            Transform target = GameState.ActivePlayer.FiringPoint.transform;
            Transform transform = GameState.ActivePlayer.CannonPivot.transform;

            float upAngle = Vector3.Angle(transform.up, target.up);

            MonoProjectileEntity projectileEntity = EntityManager.Spawn<MonoProjectileEntity>(
                key: "MachineGun",
                targetPosition: target.position,
                upAngleDegrees: upAngle,
                onSpawnEvent: 
                delegate (MonoEntity x)
                {
                    MonoProjectileEntity projectileEntity = x as MonoProjectileEntity;
                    projectileEntity.TargetDirection = GameState.ActivePlayer.Direction.normalized;
                } 
            );
        }
    }
}
