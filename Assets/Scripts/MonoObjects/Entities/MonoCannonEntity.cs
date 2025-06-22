using System.Collections;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoCannonEntity : MonoEntity
    {
        public float CannonRotationSpeed = 10;
        public Transform CannonPivot;
        public Transform FiringPoint;

        public void Update()
        {
            Vector3 dir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CannonPivot.position;

            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            CannonPivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}
