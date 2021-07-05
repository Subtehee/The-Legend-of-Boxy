// ============================
// 수정 : 2021-07-05
// 작성 : sujeong
// ============================

using System;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : Controller
    {
        public float StepOffset = 0.3f;
        public float ClimbableOffset = 0.6f;
        public float MinClimbAngle = 50.0f;
        public float MaxClimbAngle = 120.0f;
        public float ObserveWallAngle = 45.0f;
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
            CheckClimbable();
        }

        public override void FixedUpdateControl()
        {
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
                rotation = InputManager.Instance.CameraInput * CameraSensitivity;

            return rotation;
        }

        private void CheckClimbable()
        {

            Vector3 _moveDirection = Character.moveDirection.normalized;

            RaycastHit hitFromBottom;
            if (Physics.Raycast(transform.position + Vector3.up * StepOffset, _moveDirection, out hitFromBottom, MaxRayDistance, StaticLayer))
            {
                // 등반 가능한지 체크
                if (WallHitted && !Climbable)
                {
                    float angle = MeasureAngle(hitFromBottom.normal, -_moveDirection);
                    if (angle < ObserveWallAngle)
                        Climbable = true;

                }
                // 등반 중일 때
                else if (Climbable)
                {

                }
            }

            // 정상인지 체크
            RaycastHit hitFromTop;
            if (Physics.Raycast(transform.position + Vector3.up * Height, _moveDirection, out hitFromTop, MaxRayDistance, StaticLayer))
            {
                ReachTheTop = false;
                return;
            }
            else if (Climbable)
            {
                ReachTheTop = true;
            }
            else
            {
                Climbable = false;
                ReachTheTop = false;
            }
        }

        protected override void OnCollisionStay(Collision collision)
        {

            if (collision.transform.CompareTag("STATICMESH"))
            {
                Hitted = true;

                // 등반 상태가 아닐 시 등반 가능 각도인지 측정
                if (!Climbable)
                {
                    Vector3 hitNormal = collision.contacts[0].normal;
                    float wallAngle = MeasureAngle(hitNormal, Vector3.up);
                    if (wallAngle > MinClimbAngle && wallAngle < MaxClimbAngle)
                        WallHitted = true;
                    else WallHitted = false;
                }
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            try
            {
                Gizmos.DrawRay(transform.position + Vector3.up * StepOffset, Character.moveDirection * MaxRayDistance);
                Gizmos.DrawRay(transform.position + Vector3.up * Height, Character.moveDirection * MaxRayDistance);
            }
            catch (Exception e)
            {
                // null Exception //
            }
        }
    }
}

