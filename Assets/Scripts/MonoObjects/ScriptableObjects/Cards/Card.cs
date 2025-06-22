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
        public int Count;

        public MonoCard MonoCard => m_MonoCardRef;
        private MonoCard m_MonoCardRef;

        public void SetGameCard(MonoCard monoCard)
        {
            m_MonoCardRef = monoCard;
        }

        public void Clone(Card other)
        {
            CardImage = other.CardImage;
            Damage = other.Damage;
            Count = other.Count;
        }

        public virtual string GetCardDetails()
        {
            return $"Card Name: {GetType().Name}\n" +
                   $"Damage: {Damage}\n" +
                   $"Count: {Count}\n";
        }

        public Sprite GetCardImage()
        {
            return CardImage;
        }

        public int GetDamage()
        {
            int damage = Damage;
            return damage;
        }

        public int GetCount()
        {
            int count = Count;
            return count;
        }

        public virtual void OnActivate()
        {

        }
    }
}