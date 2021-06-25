// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters
{
    public class Controller : MonoBehaviour
    {

        public Character Character = null;
        public LayerMask StaticLayer = 0;

        protected Quaternion charQuaternion = Quaternion.identity;
        protected Rigidbody _rigidbody = null;

        protected virtual void Awake()
        {
            if (StaticLayer == 0)
                StaticLayer = LayerMask.NameToLayer("STATICMESH");

            _rigidbody = GetComponent<Rigidbody>();
        }

        public virtual void UpdateControl() { }         // Update()
        public virtual void FixedUpdateControl() { }    // FixedUpdate()
        public virtual Quaternion GetMoveDirection() { return charQuaternion; }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, -transform.up);
        }

        // Control Character
        public bool IsGrounded()
        {
            Debug.Log("Raycasting...");

            RaycastHit hit;
            if(Physics.Raycast(transform.position, -transform.up, out hit, 0.1f, StaticLayer))
            {
                Debug.Log("RayCasting is Hit");
                if (_rigidbody.velocity.y < float.Epsilon && hit.distance < 0.05f)
                    return true;

                Debug.Log(Character);
            }
            return false;
        }

    }
}

