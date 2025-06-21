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
        private MonoCard m_MonoCardRef;

        public void SetGameCard(MonoCard monoCard)
        {
            m_MonoCardRef = monoCard;
        }
    }
}