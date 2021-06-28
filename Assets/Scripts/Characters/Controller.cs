// ============================
// 수정 : 2021-06-28
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters
{
    public class Controller : MonoBehaviour
    {

        public Character Character = null;
        public LayerMask StaticLayer = 0;
        public float MaxRayDistance = 3.0f;

        protected Quaternion charQuaternion = Quaternion.identity;
        protected CapsuleCollider m_collider = null;

        protected float m_angleOfGround = 0.0f;

        protected virtual void Awake()
        {
            if (StaticLayer == 0)
                StaticLayer = LayerMask.NameToLayer("STATICMESH");

            m_collider = GetComponent<CapsuleCollider>();
        }

        public virtual void UpdateControl() { }         // Update()
        public virtual void FixedUpdateControl() { }    // FixedUpdate()
        public virtual Quaternion GetMoveDirection() { return charQuaternion; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -transform.up);
        }

        public float CheckGround()
        {

            RaycastHit hit;
            if (Physics.Raycast(m_collider.center, -transform.up, out hit, MaxRayDistance, StaticLayer))
            {
                if (hit.distance < float.Epsilon)
                    return 0.0f;
                return hit.distance;
            }

            return MaxRayDistance;
        }

    }
}

