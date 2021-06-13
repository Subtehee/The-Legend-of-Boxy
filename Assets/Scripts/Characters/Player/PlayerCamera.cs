// ============================
// 수정 : 2021-06-13
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        public Transform Pivot = null;
        public Transform Socket = null;

        [Header("CameraSetting")]
        public float MaxLengthFromTarget = 3.0f;
        public float SmoothDamp = 7.0f;
        public float CollisionRadius = 0.2f;
        public float MinPitchAngle = -45.0f;
        public float MaxPitchAngle = 75.0f;
        public float TargetOffset = 1.6f;

        public readonly LayerMask staticLayer = 0;

        private Vector3 rigVelocity = Vector3.zero;
        private Vector3 socketVelocity = Vector3.zero;

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
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition + (Vector3.up * TargetOffset),
                ref rigVelocity, SmoothDamp * Time.fixedDeltaTime);

            // Set socket position
            Vector3 socketTargetPos = transform.position + (-Pivot.forward * GetTargetLength());
            Socket.position = Vector3.SmoothDamp(Socket.position, socketTargetPos,
                ref socketVelocity, SmoothDamp * Time.fixedDeltaTime);
        }

        // 타겟과의 거리 측정
        private float GetTargetLength()
        {
            RaycastHit hit;
            if (Physics.SphereCast(transform.position, CollisionRadius, -transform.forward, out hit, MaxLengthFromTarget))
            {
                Debug.Log("hitted");
                return hit.distance;
            }
            else return MaxLengthFromTarget;
        }

        // 카메라 회전
        public void SetCameraRotation(Vector2 targetRotation)
        {
            float yawAngle = (-targetRotation.x + transform.eulerAngles.y) % 360;
            float pitchAngle = (targetRotation.y + Pivot.localEulerAngles.x) % 360;

            pitchAngle = Mathf.Clamp(pitchAngle, MinPitchAngle, MaxPitchAngle);

            Debug.Log("targetRotationY : " + targetRotation.y + " / Angle : "+Pivot.localEulerAngles+ " / PitchAngle : " + pitchAngle);

            if (targetRotation.sqrMagnitude > 0.0f)
            {
                // Set rig rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0.0f, yawAngle, 0.0f),
                    SmoothDamp * Time.fixedDeltaTime);

                // Set pivot rotation
                Pivot.localRotation = Quaternion.Slerp(Pivot.localRotation, Quaternion.Euler(pitchAngle, 0.0f, 0.0f),
                    SmoothDamp * Time.fixedDeltaTime);
            }
            else
            {
                transform.rotation = transform.rotation;
                Pivot.localRotation = Pivot.localRotation;
            }

            Debug.Log("PitchAngle : " + Pivot.localEulerAngles.x);
        }

        // 현재의 회전값 가져오기
        public Vector2 GetControlRotation()
        {
            return new Vector2(transform.eulerAngles.y, Pivot.localEulerAngles.x);
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

