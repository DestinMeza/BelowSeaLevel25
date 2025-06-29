using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public class MonoEffectEntity : MonoEntity
    {
        public SpriteRenderer spriteRenderer;

        private List<Sprite> m_Sprites;
        private float m_AnimationSpeed;


        public override void OnEnable()
        {
            StartCoroutine(PlayEffect());
        }

        public override void Init(Entity entity)
        {
            base.Init(entity);

            var effect = entity as IEffect;
            m_Sprites = effect.GetSprites();
            m_AnimationSpeed = effect.GetAnimationSpeed();
        }

        private IEnumerator PlayEffect()
        {
            yield return new WaitForEndOfFrame();

            for (int i = 0; i < m_Sprites.Count; i++)
            {
                spriteRenderer.sprite = m_Sprites[i];
                yield return new WaitForSeconds(m_AnimationSpeed);
            }

            spriteRenderer.sprite = m_Sprites[0];
            gameObject.SetActive(false);
        }
    }
}
