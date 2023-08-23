using System;
using UnityEngine;


namespace Movementsystem
{
    [Serializable]

    public class PlayerRunDate
    {
        [field: SerializeField][field: Range(1f, 2f)] public float SpeedModifier { get; private set; } = 1f;

    }
}