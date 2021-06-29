// ============================
// 수정 : 2021-06-28
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

        [HideInInspector] public bool Hitted = false;
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
                if (hit.distance < float.Epsilon)
                    return 0.0f;
                return hit.distance;
            }
            return MaxRayDistance;
        }

        protected void OnCollisionEnter(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
            {
                Hitted = true;
                Debug.Log("Hitted with StaticMEsh");
            }
        }

        protected void OnCollisionExit(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
                Hitted = false;
            Debug.Log("Exit fromm StaticMesh");
        }
    }
}

