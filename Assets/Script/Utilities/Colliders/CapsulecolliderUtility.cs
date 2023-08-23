using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Movementsystem
{
    [Serializable]
    public class CapsulecolliderUtility
    {
        public CapsuleColliderData CapsuleColliderData { get; private set; }
        [field:SerializeField]public DefaultColliderData DefaultColliderData { get; private set; }
        [field: SerializeField] public SlopeData slopeData  { get; private set; }

        public void CalculateCapsulecolliderDimensions() 
        {
            SetCapsuleColliderRadius(DefaultColliderData.Radius);
            SteCapsulColliderHeight(DefaultColliderData.Height);
        }

        public void SetCapsuleColliderRadius(float radius)
        {
            CapsuleColliderData.Collider.radius = radius;
        }
        public void SetCapsuleColliderHeight(float height)
        {
            CapsuleColliderData.Collider.height = height;
        }
    }
}