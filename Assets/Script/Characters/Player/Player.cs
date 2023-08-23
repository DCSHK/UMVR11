using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Movementsystem
{
    [RequireComponent(typeof(PlayerInput))]
    public class Player : MonoBehaviour
    {
        [field:Header("References")]
        [field:SerializeField] public PlayerSO Date { get; private set; }

        [field: Header("Collisions")]
        [field:SerializeField] public CapsulecolliderUtility ColliderUtiliry { get; private set; }
        [field: SerializeField] public PlayerLayerData LayerData { get; private set; }

       public Rigidbody Rigidbody { get; private set; }

        public Transform MainCameraTransform { get; private set; }
        public PlayerInput Input { get; private set; }
        private PlayerMovementStateMachine movementStateMachine;
        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            Input = GetComponent<PlayerInput>();
            ColliderUtiliry.Initialize(gameObject);
            ColliderUtiliry.CalculateCapsulecolliderDimensions();
            MainCameraTransform = Camera.main.transform;
            movementStateMachine = new PlayerMovementStateMachine(this);
        }

        private void OnValidate()
        {
            ColliderUtiliry.Initialize(gameObject);
            ColliderUtiliry.CalculateCapsulecolliderDimensions();
        }
        private void Start()
        {
            movementStateMachine.ChangeState(movementStateMachine.IdlingState);
        }
        private void Update()
        {
            movementStateMachine.Handleinput();
            movementStateMachine.Update();
        }
        private void FixedUpdate()
        {
            movementStateMachine.PhysicsUpdate();
        }
    }
}
