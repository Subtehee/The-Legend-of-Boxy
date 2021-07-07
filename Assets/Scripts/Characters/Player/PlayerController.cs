// ============================
// 수정 : 2021-07-07
// 작성 : sujeong
// ============================

using System;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : Controller
    {
        //public float StepOffset = 0.3f;
        //public float ClimbableOffset = 0.6f;
        //public float MinClimbAngle = 50.0f;
        //public float MaxClimbAngle = 120.0f;
        //public float ObserveWallAngle = 45.0f;
        /*[HideInInspector]*/
        public bool Climbable = false;
        /*[HideInInspector]*/
        public bool ReachTheTop = false;
        [HideInInspector] public bool Climbing = false;

        [Header("Camera Setting")]
        public PlayerCamera PlayerCamera = null;
        [Range(1.0f, 10.0f)] public float CameraSensitivity = 5.0f;

        private Transform player = null;
        private CapsuleCollider m_collider = null;
        private bool WallHitted = false;
        private float Height = 1.8f;

        protected override void Awake()
        {
            base.Awake();
            player = transform;
            m_collider = GetComponent<CapsuleCollider>();
        }

        private void Start()
        {
            Height = m_collider.height;
        }

        public override void UpdateControl()
        {
            InputManager.Instance.UpdateInputs();
        }

        public override void LateUpdateControl()
        {
            base.LateUpdateControl();
        }

        public override void FixedUpdateControl()
        {
            PlayerCamera.SetCameraPosition(player.position);
            PlayerCamera.SetCameraRotation(GetTargetRotation());
        }

        public override Vector3 GetMoveDirection(Vector2 targetDir, Quaternion curRotation)
        {
            // Update rotation ba  
            Character.curRotation = PlayerCamera.transform.rotation;
            Character.targetDirection = InputManager.Instance.MoveInput;     

            Vector3 yawVector = ((curRotation * Vector3.forward * targetDir.y)
                            + (curRotation * Vector3.right * targetDir.x)).normalized;

            // 지면의 경사에 따라 pitch축 조절
            moveDirection = CalculateDirectionSlope(yawVector, normalOfGround);

            return moveDirection;
        }

        private Vector2 GetTargetRotation()
        {
            Vector2 rotation = Vector2.zero;

            if (InputManager.Instance.HasCameraInput)
                rotation = InputManager.Instance.CameraInput * CameraSensitivity;

            return rotation;
        }
    }
}

