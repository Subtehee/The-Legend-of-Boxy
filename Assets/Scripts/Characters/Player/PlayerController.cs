// ============================
// 수정 : 2021-06-22
// 작성 : sujeong
// ============================

using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : Controller
    {
        public PlayerCamera PlayerCamera = null;

        [Header("User Setting")]
        [Range(1.0f, 4.0f)]public float CameraSensitivity = 2.0f;

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
            InputManager.Instance.UpdateInputs();
        }

        public Transform GetPlayerDirection() => PlayerCamera.transform;

        public override void FixedUpdateControl()
        {
            base.FixedUpdateControl();
            PlayerCamera.SetCameraPosition(player.position);
            PlayerCamera.SetCameraRotation(GetTargetRotation());
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, -transform.up);
        }

        private Vector2 GetTargetRotation()
        {
            Vector2 rotation = Vector2.zero;

            if (InputManager.Instance.HasCameraInput)
            {
                rotation.x = InputManager.Instance.CameraInput.x * CameraSensitivity;   // yaw
                rotation.y = InputManager.Instance.CameraInput.y * CameraSensitivity;   // pitch
            }
            return rotation;
        }

        // 접지 상태 체크
    }
}

