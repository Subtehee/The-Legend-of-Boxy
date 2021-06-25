// ============================
// 수정 : 2021-06-25
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
        [Range(1.0f, 10.0f)] public float CameraSensitivity = 5.0f;

        private Transform player = null;

        protected override void Awake()
        {            
            base.Awake();
            player = transform;
        }

        public override void UpdateControl()
        {
            InputManager.Instance.UpdateInputs();
        }

        public override void FixedUpdateControl()
        {
            base.FixedUpdateControl();

            PlayerCamera.SetCameraPosition(player.position);
            PlayerCamera.SetCameraRotation(GetTargetRotation());
        }

        public override Quaternion GetMoveDirection()
        {
            charQuaternion = PlayerCamera.transform.rotation;

            return base.GetMoveDirection();
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
    }
}

