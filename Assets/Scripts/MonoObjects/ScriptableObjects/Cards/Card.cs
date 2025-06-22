using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    public interface ICardable
    {
        /// <summary>
        /// This assigns the card index of the corrisponding mono object
        /// </summary>
        /// <param name="index"></param>
        public void SetGameCard(MonoCard monoCard);
    }

    public abstract class Card : ScriptableObject, ICardable
    {
        public Sprite CardImage;
        public int Damage;

        public MonoCard MonoCard => m_MonoCardRef;
        private MonoCard m_MonoCardRef;

        public void SetGameCard(MonoCard monoCard)
        {
            m_MonoCardRef = monoCard;
        }

        public virtual string GetCardDetails()
        {
            return $"Card Name: {GetType().Name}\n" +
                   $"Damage: {Damage}\n";
        }

        public virtual Sprite GetCardImage()
        {
            return CardImage;
        }

        public virtual int GetDamage()
        {
            return Damage;
        }

        public virtual void OnActivate()
        {
            
        }
    }
}