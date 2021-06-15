// ============================
// 수정 : 2021-06-15
// 작성 : sujeong
// ============================

using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : Controller
    {
        public PlayerInput PlayerInput = null;
        public PlayerCamera PlayerCamera = null;

        [Header("User Setting")]
        [Range(1, 10)]public float CameraSensitivity = 7.0f;

        [HideInInspector] public Vector3 MoveDirection = Vector3.zero;      // Player move direction

        private Transform player = null;

        protected override void Awake()
        {
            base.Awake();
            player = transform;
        }

        private void OnEnable()
        {
            IsGrounded = true;
        }

        public override void UpdateControl()
        {
            PlayerInput.UpdateInputs();

            SetCharacterState();
        }

        public override void FixedUpdateControl()
        {
            base.FixedUpdateControl();
            PlayerCamera.SetCameraPosition(player.position);
            PlayerCamera.SetCameraRotation(GetTargetRotation());
        }

        // 입력값에 따른 상태 측정
        protected override void SetCharacterState()
        {

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, -transform.up);
        }

        private Vector2 GetTargetRotation()
        {
            Vector2 rotation = Vector2.zero;

            if (PlayerInput.HasCameraInput)
            {
                rotation.x = PlayerInput.CameraInput.x * CameraSensitivity;   // yaw
                rotation.y = PlayerInput.CameraInput.y * CameraSensitivity;   // pitch
            }
            return rotation;
        }
    }
}

