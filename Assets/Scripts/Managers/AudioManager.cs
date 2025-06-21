using System;
using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    internal class AudioManager : MonoManager<AudioManager>
    {
        public InitalizationTable<AudioObject> audioMap;

        public override void Init()
        {
            base.Init();

        }

        public static void PlayClip(String clipName)
        {

        }
    }
}
