using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

using static BelowSeaLevel_25.Globals;

namespace BelowSeaLevel_25.AI
{
    internal class AnglerFishTree : BehaviorTree
    {
        public const float ATTACK_DIST = 2;
        public AnglerFishTree(MonoEntity monoEntity) : base(monoEntity,
            new SelectorNode(
                new ConditionalNode(() => Vector3.Distance(monoEntity.gameObject.transform.position, GameState.PlayerStartingPosition) > ATTACK_DIST, new MoveToDomeNode()),
                new AttackDomeNode()
            ))
        {

        }
    }

}