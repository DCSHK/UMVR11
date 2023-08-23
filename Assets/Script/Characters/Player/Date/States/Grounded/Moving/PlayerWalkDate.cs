using System;
using UnityEngine;

namespace Movementsystem
{
    [Serializable]

    public class PlayerWalkDate
    {
        [field: SerializeField][field: Range(0f, 1f)] public float SpeedModifier { get; private set; } = 0.225f;
    }
}