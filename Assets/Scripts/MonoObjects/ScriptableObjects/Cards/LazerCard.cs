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

            float upAngle = Vector3.Angle(transform.up, target.up);
            MonoProjectileEntity projectileEntity = EntityManager.Spawn("Lazer", target.position, upAngle) as MonoProjectileEntity;
            projectileEntity.TargetDirection = transform.up;
        }
    }
}
