using System;
using System.Collections.Generic;
using UnityEngine;

namespace BelowSeaLevel_25
{
    internal class DebugManager : MonoManager<DebugManager>
    {
        public override void Init()
        {
            base.Init();
            Debug.Log("Hello World!");
        }
    }
}
