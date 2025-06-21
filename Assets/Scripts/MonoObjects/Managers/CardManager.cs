using System.Collections.Generic;
using System.Linq;
using UnityEngine.Rendering;

namespace BelowSeaLevel_25
{
    internal class CardManager : MonoManager<CardManager>
    {
        public MonoHand GameHand;

        public override void Init()
        {
            base.Init();
            GameHand.Init();
        }

        public static void Draw()
        {
            Instance.GameHand.Draw();
        }
    }
}