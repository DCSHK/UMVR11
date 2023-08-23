using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SlopeData
{
    [field: SerializeField][field: Range(0f, 1f)] public float StepHeightPercantage { get; private set; } = 0.25f;
}
