using UnityEngine;
using UnityEngine.InputSystem;

namespace Movementsystem
{
    public class PlayerMovementState : IState
    {
        protected PlayerMovementStateMachine stateMachine;

        protected PlayerGroundedDate movementDate;


        //private float rotationSpeed = 600f; //這行是後面加的

        public PlayerMovementState(PlayerMovementStateMachine playerMovementStateMachine)
        {
            stateMachine = playerMovementStateMachine;

            movementDate = stateMachine.Player.Date.GroundedDate;
            InitializeDate();
        }

        private void InitializeDate()
        {
            stateMachine.ReusableDate.TimeToReachTargetRotation = movementDate.BaseRotationDate.TargetRotationReachTime;
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
                    stateMachine.ReusableDate.MovementInput = stateMachine.Player.Input.PlayerActions.Movement.ReadValue<Vector2>();
        }
        private void Move()
        {
            if (        stateMachine.ReusableDate.MovementInput == Vector2.zero || stateMachine.ReusableDate.MovementSpeedModifier==0f) 
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
            stateMachine.ReusableDate.CurrentTargetRotation.y = targetAngle;
            stateMachine.ReusableDate.DampedTargetRotationPassedTime.y = 0f;
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
            return new Vector3(        stateMachine.ReusableDate.MovementInput.x, 0f,         stateMachine.ReusableDate.MovementInput.y);
        }
        protected float GetMovementSpeed()
        {
            return movementDate.BaseSpeed * stateMachine.ReusableDate.MovementSpeedModifier;
        }
        protected Vector3 GetPlayerHorizontalVelocity()
        {
            Vector3 playerHorizontalVelocity = stateMachine.Player.Rigidbody.velocity;
            playerHorizontalVelocity.y = 0f;
            return playerHorizontalVelocity;
        }
        protected Vector3 GetPlayerVerticalVelocity()
        {
            return new Vector3 (0f,stateMachine.Player.Rigidbody.velocity.y, 0f);
        }

        protected void RotateTowardsTargetRotation()
        {
            //float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            //float targetYAngle = stateMachine.ReusableDate.CurrentTargetRotation.y;

            //// 計算角度差
            //float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentYAngle, targetYAngle));

            //// 設定合適的 rotationSpeed
            //float rotationSpeed = angleDifference / stateMachine.ReusableDate.TimeToReachTargetRotation.y * 3.05f;

            //Quaternion currentRotation = Quaternion.Euler(0f, currentYAngle, 0f);
            //Quaternion targetRotation = Quaternion.Euler(0f, targetYAngle, 0f);

            //Quaternion newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            //stateMachine.Player.Rigidbody.MoveRotation(newRotation);
            float currentYAngle = stateMachine.Player.Rigidbody.rotation.eulerAngles.y;
            if (currentYAngle == stateMachine.ReusableDate.CurrentTargetRotation.y)
            {
                return;
            }
            float smoothedYAngle = Mathf.SmoothDampAngle(currentYAngle, stateMachine.ReusableDate.CurrentTargetRotation.y, ref stateMachine.ReusableDate.DampedTargetRotationCurrentVelocity.y, stateMachine.ReusableDate.TimeToReachTargetRotation.y, stateMachine.ReusableDate.DampedTargetRotationPassedTime.y * 10000f);
            stateMachine.ReusableDate.DampedTargetRotationPassedTime.y += Time.deltaTime;
            Quaternion targetRotation = Quaternion.Euler(0f, smoothedYAngle, 0f);
            stateMachine.Player.Rigidbody.MoveRotation(targetRotation);
        }

        protected float UpdateTargetRotation(Vector3 direction, bool shouldConsiderCameraRotatoion = true)
        {
            float directionAngle = GetDirectionAngle(direction);
            if (shouldConsiderCameraRotatoion)
            {
                directionAngle = AddCameraRotationAngle(directionAngle);
            }

            

            if (directionAngle != stateMachine.ReusableDate.CurrentTargetRotation.y)
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
            stateMachine.ReusableDate.shouldWalk = !stateMachine.ReusableDate.shouldWalk;
        }
        #endregion
    }
}