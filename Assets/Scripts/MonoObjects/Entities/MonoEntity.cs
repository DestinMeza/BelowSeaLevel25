using System;
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

        /// <summary>
        /// On Spawn happens before OnEnable.
        /// </summary>
        public virtual void OnSpawn(Action<MonoEntity> action)
        {
            action(this);
        }

        public virtual void OnEnable()
        {

        }
        
        public virtual void OnDisable()
        {

        }
    }
}