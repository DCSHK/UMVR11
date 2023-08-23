using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Movementsystem
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;
        protected Vector2 movementInput;
        protected float baseSpeed = 5f;
        protected float speedModifier = 1f;

        protected Vector3 currentTargetRotation;
        protected Vector3 timeToReachTargetRotation;
        protected Vector3 dampedTargetRotationCurrentVelocity;
        protected Vector3 dampedTargetRotationPassedTime;

        protected bool shouldWalk;
        private float rotationSpeed = 600f; //這行是後面加的

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;
            InitializeDate();
        }

        private void InitializeDate()
        {
            timeToReachTargetRotation.y = 0.14f;
        }

        public virtual void Enter()
        {
            Debug.Log("State: "+ GetType().Name);
            AddInputActionCallbacks();
        }



        public virtual void Exit()
        {
            RemoveInputActionCallbacks();
        }



        public virtual void Handleinput()
        {
            ReadMovementInput();
        }


        public virtual void Update()
        {

        }
        public virtual void PhysicsUpdate()
        {
            Move();
        }
        #region Main Methods
        public void ReadMovementInput()
        {
            movementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }
        private void Move()
        {
            if (movementInput == Vector2.zero || speedModifier==0f) 
            {
                return;
            }
            Vector3 movementDirection = GetMovementInputDirection();

            float targetRotationYAngle = Rotate(movementDirection);
            Vector3 targetRotationDirection = GetTargetRotationDirection(targetRotationYAngle);
            float movementSpeed = GetMovementSpeed();

            Vector3 currentPlayerHorizontalVelocity = GetPlayerHorizontalVelocity();
            stateMachine.Player.Rigidbody.AddForce(targetRotationDirection *movementSpeed -currentPlayerHorizontalVelocity , ForceMode.VelocityChange);
        }
        private float Rotate(Vector3 direction)
        {
            float directionAngle = UpdateTargetRotation(direction);
            RotateTowardsTargetRotation();
            return directionAngle;
        }
        private float AddCameraRotationAngle(float angle)
        {
            angle += stateMachine.Player.MainCameraTransform.eulerAngles.y;
            if (angle > 360f)
            {
                angle -= 360f;
            }

            return angle;
        }
        private void UpdateTargerRotationDate(float targetAngle)
        {
            currentTargetRotation.y = targetAngle;
            dampedTargetRotationPassedTime.y = 0f;
        }
        private float GetDirectionAngle(Vector3 direction)
        {
            float directionAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            if (directionAngle < 0f)
            {
                directionAngle += 360f;
            }

            return directionAngle;
        }

        #endregion
        #region Reuseable Methods
        protected Vector3 GetMovementInputDirection()
        {
            return new Vector3(movementInput.x, 0f, movementInput.y);
        }
        protected float GetMovementSpeed()
        {
            return baseSpeed * speedModifier;
        }
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;
            return playerHorizontalVelocity;
        }

        protected void RotateTowardsTargetRotation()
        {
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            float targetYAngle = currentTargetRotation.y;

            // 計算角度差
            float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentYAngle, targetYAngle));

            // 設定合適的 rotationSpeed
            float rotationSpeed = angleDifference / timeToReachTargetRotation.y * 3.05f;

            Quaternion currentRotation = Quaternion.Euler(0f, currentYAngle, 0f);
            Quaternion targetRotation = Quaternion.Euler(0f, targetYAngle, 0f);

            Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            stateMachine.Player.Rigidbody.MoveRotation(newRotation);
            //float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            //if (currentYAngle == currentTargetRotation.y)
            //{
            //    return;
            //}
            //float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, currentTargetRotation.y, ref dampedTargetRotationCurrentVelocity.y , timeToReachTargetRotation.y , dampedTargetRotationPassedTime.y * 10000f);
            //dampedTargetRotationPassedTime.y += Time.deltaTime;
            //Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle , 0f);
            //stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotatoion = true)
        {
            float directionAngle = GetDirectionAngle(direction);
            if (shouldConsiderCameraRotatoion)
            {
                directionAngle = AddCameraRotationAngle(directionAngle);
            }

            

            if (directionAngle != currentTargetRotation.y)
            {
                UpdateTargerRotationDate(directionAngle);
            }

            return directionAngle;
        }
        protected Vector3 GetTargetRotationDirection(float targetAngle)
        {
            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        protected void ResetVelocity()
        {
            stateMachine.Player.Rigidbody.velocity = Vector3.zero;
        }
        protected virtual void AddInputActionCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started += OnWalkToggleStarted;
        }



        protected virtual void RemoveInputActionCallbacks()
        {
            stateMachine.Player.Input.PlayerActions.WalkToggle.started -= OnWalkToggleStarted;
        }
        #endregion
        #region Input Methobs
        protected virtual void OnWalkToggleStarted(InputAction.CallbackContext context)
        {
            shouldWalk = !shouldWalk;
        }
        #endregion
    }
}