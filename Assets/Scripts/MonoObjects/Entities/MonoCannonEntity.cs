using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoCannonEntity : MonoEntity
    {
        public float CannonRotationSpeed = 10;
        public SpriteRenderer FiringPointSpriteRenderer;
        public SpriteRenderer CannonSpriteRenderer;
        public Transform CannonPivot;
        public Transform FiringPoint;
        public Vector3 Direction;

        private float m_AnimationSpeed;
        private Sprite m_CannonSprite;
        private Sprite m_DomeSprite;
        private List<Sprite> m_CannonFiringSprites;
        private List<Sprite> m_FireEffectSprites;
        private List<Sprite> m_LazerEffectSprites;
        private Coroutine m_ActiveCannonCoroutine;
        private Coroutine m_ActiveFiringPointCoroutine;

        public void Update()
        {
            Direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - CannonPivot.position;

            float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
            CannonPivot.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        public override void Init(Entity entity)
        {
            base.Init(entity);

            CannonEntity cannonEntity = entity as CannonEntity;
            m_CannonSprite = cannonEntity.GetBarrel();
            m_DomeSprite = cannonEntity.GetBarrelBase();
            m_CannonFiringSprites = cannonEntity.GetCannonFiringSprites();
            m_FireEffectSprites = cannonEntity.GetFireEffectSprites();
            m_LazerEffectSprites = cannonEntity.GetLazerEffectSprites();
            m_AnimationSpeed = cannonEntity.GetAnimationSpeed();
        }

        public void PlayCannonEffect()
        {
            if (m_ActiveCannonCoroutine != null)
            {
                StopCoroutine(m_ActiveCannonCoroutine);
            }

            m_ActiveCannonCoroutine = StartCoroutine(PlayCannonFiringAnimation());
        }

        public void PlayFiringEffect()
        {
            if (m_ActiveFiringPointCoroutine != null)
            {
                StopCoroutine(m_ActiveFiringPointCoroutine);
            }

            m_ActiveFiringPointCoroutine = StartCoroutine(PlayFireEffect());
        }

        public void PlayFiringEffectLazer()
        {
            if (m_ActiveFiringPointCoroutine != null)
            {
                StopCoroutine(m_ActiveFiringPointCoroutine);
            }

            m_ActiveFiringPointCoroutine = StartCoroutine(PlayLazerEffect());
        }

        private IEnumerator PlayCannonFiringAnimation()
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < m_CannonFiringSprites.Count; i++)
            {
                CannonSpriteRenderer.sprite = m_CannonFiringSprites[i];
                yield return new WaitForSeconds(m_AnimationSpeed);
            }

            CannonSpriteRenderer.sprite = m_CannonSprite;
            m_ActiveCannonCoroutine = null;
        }

        private IEnumerator PlayFireEffect()
        {
            yield return new WaitForEndOfFrame();
            FiringPointSpriteRenderer.color = new Color(1, 1, 1, 1);

            for (int i = 0; i < m_FireEffectSprites.Count; i++)
            {
                FiringPointSpriteRenderer.sprite = m_FireEffectSprites[i];
                yield return new WaitForSeconds(m_AnimationSpeed);
            }

            FiringPointSpriteRenderer.color = new Color(1, 1, 1, 0);
            m_ActiveFiringPointCoroutine = null;
        }

        private IEnumerator PlayLazerEffect()
        {
            yield return new WaitForEndOfFrame();
            FiringPointSpriteRenderer.color = new Color(1, 1, 1, 1);

            for (int i = 0; i < m_LazerEffectSprites.Count; i++)
            {
                FiringPointSpriteRenderer.sprite = m_LazerEffectSprites[i];
                yield return new WaitForSeconds(m_AnimationSpeed);
            }

            FiringPointSpriteRenderer.color = new Color(1, 1, 1, 0);
            m_ActiveFiringPointCoroutine = null;
        }
    }
}
