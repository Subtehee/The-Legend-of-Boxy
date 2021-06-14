// ============================
// 수정 : 2021-06-14
// 작성 : sujeong
// ============================

using System.Collections;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Transform _player = null;
        public PlayerInput PlayerInput = null;
        public PlayerCamera PlayerCamera = null;

        public LayerMask staticLayer = 0;
        public PlayerState State = new PlayerState();   // Player state FSM

        [Header("User Setting")]
        [Range(1, 10)]public float CameraSensitivity = 7.0f;

        [HideInInspector] public Vector3 MoveDirection = Vector3.zero;      // Player move direction

        public bool IsJumping { get; set; }
        public bool IsDashing { get; set; }

        public bool IsMoving { get; set; }      // has user keyboard input
        private bool IsGrounded = true;

        private void Awake()
        {
            if (_player == null)
                _player = transform;
        }

        private void OnEnable()
        {
            IsGrounded = true;
        }

        public void UpdateControl()
        {
            PlayerInput.UpdateInputs();

            SetCharacterState();
        }

        public void FixedUpdateControl()
        {
            CheckGrounded();

            PlayerCamera.SetCameraPosition(_player.position);
            PlayerCamera.SetCameraRotation(GetTargetRotation());
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


        // 입력값에 따른 상태 측정
        private void SetCharacterState()
        {
            if(IsGrounded)
            {

            }
            else
            {

            }
        }

        private void CheckGrounded()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, -transform.up);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 1.0f, staticLayer))
            {
                if (hit.distance < float.Epsilon)
                    IsGrounded = true;
            }
            else IsGrounded = false;
        }


        public Vector3 GetMoveDirection()
        {
            return PlayerCamera.transform.forward;
        }
    }
}

