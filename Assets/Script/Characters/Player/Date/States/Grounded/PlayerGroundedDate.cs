using System;
using UnityEngine;

namespace Movementsystem
{
    [Serializable]
    public class PlayerGroundedDate
    {
        [field: SerializeField][field:Range(0f,25f)] public float BaseSpeed { get; private set; } = 5f;
        [field: SerializeField]public PlayerRotationDate BaseRotationDate { get; private set; }
        [field: SerializeField]public PlayerWalkDate WalkDate { get; private set; }
        [field: SerializeField] public PlayerRunDate RunDate { get; private set; }

    }
}
