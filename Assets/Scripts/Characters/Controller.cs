// ============================
// 수정 : 2021-06-30
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class Controller : MonoBehaviour
    {

        public Character Character = null;
        public LayerMask StaticLayer = 0;

        [HideInInspector] public bool Movable = false;
        [HideInInspector] public bool Hitted = false;

        [Header("Movable Setting")]
        public float MaxUphillAngle = 55.0f;
        public float MaxRayDistance = 5.0f;
        public float RayOffset = 0.1f;

        protected Quaternion charQuaternion = Quaternion.identity;
        protected float m_angleOfGround = 0.0f;

        protected virtual void Awake()
        {
            if (StaticLayer == 0)
                StaticLayer = LayerMask.NameToLayer("STATICMESH");
        }

        public virtual void UpdateControl() { }         // Update()
        public virtual void LateUpdateControl() { }     // LateUpdate()
        public virtual void FixedUpdateControl() { }    // FixedUpdate()
        public virtual Quaternion GetMoveDirection() { return charQuaternion; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position + transform.up * RayOffset, -transform.up * MaxRayDistance);
        }

        public float GetDistanceFromGround()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up * RayOffset, -transform.up, out hit, MaxRayDistance, StaticLayer))
            {
                if (hit.distance <= RayOffset)
                    return 0.0f;
                return hit.distance - RayOffset;
            }
            return MaxRayDistance;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
            {
                Vector3 normalOfGround = collision.contacts[0].normal;
                CheckGrounded(normalOfGround);

                Hitted = true;
            }
        }

        private void CheckGrounded(Vector3 normal)
        {
            // 지면을 기준으로 접지된 부분의 각도 구하기
            float dot = Vector3.Dot(normal, transform.up);
            float pointAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (Mathf.Abs(pointAngle) < MaxUphillAngle)
                Movable = true;
            else  
                Movable = false;
        }

        protected void OnCollisionExit(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
            {
                Hitted = false;
                Movable = false;
            }
        }
    }
}

