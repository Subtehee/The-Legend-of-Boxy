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
        [HideInInspector] public bool Climbable = false;

        [Header("Camera Setting")]
        public PlayerCamera PlayerCamera = null;
        [Range(1.0f, 10.0f)] public float CameraSensitivity = 5.0f;

        private Transform player = null;
        private CapsuleCollider m_collider = null;

        protected override void Awake()
        {            
            base.Awake();
            player = transform;
            m_collider = GetComponent<CapsuleCollider>();
        }

        public override void UpdateControl()
        {
            InputManager.Instance.UpdateInputs();
            Debug.DrawRay(transform.position + Vector3.up * 1.0f, Character.moveDirection, Color.red);
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

        public void CheckClimbable()
        {
            // 카메라는 고정
            // 올라갈 때는 벽의 방향으로 몸도 움직임
            // 발에서 레이캐스팅을 쏘고
            // 머리에서 레이캐스팅을 쏘고
            // 둘 다 닿으면 등반
            // 머리가 안닿으면 top --> 단번에 올라가서 
            // 발만 닿으면 뛰어 오르는 애니메이션

            bool bottomHit = false;
            bool topHit = false;

            Gizmos.DrawLine(transform.position + transform.up * RayOffset, transform.forward * 2.0f);
            RaycastHit hitFromFeet;
            if(Physics.Raycast(transform.position + transform.up * RayOffset, transform.forward, out hitFromFeet, MaxRayDistance, StaticLayer))
            {
                bottomHit = true;
            }

            Gizmos.DrawLine(transform.position + transform.up * RayOffset, transform.forward * m_collider.height);
            RaycastHit hitFromHead;
            if(Physics.Raycast(transform.position + transform.up * m_collider.height, transform.forward, out hitFromHead, MaxRayDistance, StaticLayer))
            {
                topHit = true;
            }

        }
    }
}

