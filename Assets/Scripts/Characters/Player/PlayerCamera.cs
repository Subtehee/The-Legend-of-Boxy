// ============================
// 수정 : 2021-06-23
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        // for move player
        public Transform Pivot = null;      
        public Transform Socket = null;     

        [Header("Camera Setting")]
        public float MaxLengthFromTarget = 3.0f;
        public float MixLengthFromTarget = 1.0f;
        public float CollisionRadius = 0.2f;
        public float TargetOffset = 1.6f;
        public float MinPitchAngle = -45.0f;
        public float MaxPitchAngle = 75.0f;
        public float PositionDamp = 5.0f;
        public float RotationDamp = 1.0f;
        public LayerMask staticLayer = 0;

        // SmoothDamp Velocities
        private Vector3 rigVelocity = Vector3.zero;
        private Vector3 socketVelocity = Vector3.zero;
        private float yVelocity = 0.0f;
        private float xVelocity = 0.0f;

        public void Awake()
        {
            if (Pivot == null)
                Pivot = GetComponentInChildren<Transform>();
            if (Socket == null)
                Socket = Pivot.GetComponentInChildren<Transform>();
        }

        // 캐릭터의 위치 추적
        public void SetCameraPosition(Vector3 targetPosition)
        {
            // Set rig position
            transform.position = Vector3.Lerp(transform.position, targetPosition + (Vector3.up * TargetOffset), 
                                                PositionDamp * Time.fixedDeltaTime);
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition + (Vector3.up * TargetOffset),
            //    ref rigVelocity, PositionDamp * Time.deltaTime);

            // Set socket position
            Vector3 socketTargetPos = transform.position + (-Pivot.forward * GetTargetLength());
            Socket.position = Vector3.Lerp(Socket.position, socketTargetPos, PositionDamp * Time.fixedDeltaTime);

            //Socket.position = Vector3.SmoothDamp(Socket.position, socketTargetPos,
            //    ref socketVelocity, PositionDamp * Time.deltaTime);
        }

        // 타겟과의 거리 측정
        private float GetTargetLength()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, CollisionRadius, -Socket.forward,
                                    out hit, MaxLengthFromTarget, staticLayer))
                return hit.distance;

            else return MaxLengthFromTarget;
        }

        // 카메라 회전
        public void SetCameraRotation(Vector2 targetRotation)
        {
            // 목표의 오일러각
            float yawAngle = -targetRotation.x + transform.eulerAngles.y;
            float pitchAngle = targetRotation.y + Pivot.localEulerAngles.x;
            
            //pitchAngle = Mathf.Clamp(pitchAngle, MinPitchAngle, MaxPitchAngle);  

            yawAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, yawAngle,
                ref yVelocity, RotationDamp * Time.fixedDeltaTime);
            pitchAngle = Mathf.SmoothDampAngle(Pivot.localEulerAngles.x, pitchAngle,
                ref xVelocity, RotationDamp * Time.fixedDeltaTime);

            // Set rig rotation
            transform.rotation = Quaternion.Euler(0.0f, yawAngle, 0.0f);
            // Set pivot rotation
            Pivot.localRotation = Quaternion.Euler(pitchAngle, 0.0f, 0.0f);
        }

        // 카메라의 위치 기즈모
        void OnDrawGizmos()
        {
            if (Socket != null)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawLine(transform.position, Socket.position);
                Gizmos.DrawWireCube(transform.position, new Vector3(0.2f, 0.2f, 0.2f));
                Gizmos.DrawWireSphere(Socket.position, CollisionRadius);
            }
        }
    }
}

