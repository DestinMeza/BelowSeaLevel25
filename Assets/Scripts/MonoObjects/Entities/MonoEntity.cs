using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public abstract class MonoEntity : MonoBehaviour
    {
        public Entity Entity => m_EntityRef;

        private Entity m_EntityRef;

        public virtual void Init(Entity entity)
        {
            m_EntityRef = entity;
        }
        
        public virtual void OnEnable()
        {

        }
        public virtual void OnDisable()
        { 
            
        }
    }
}