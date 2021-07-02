// ============================
// ���� : 2021-07-02
// �ۼ� : sujeong
// ============================

using UnityEngine;

namespace Characters
{
    [RequireComponent(typeof(CapsuleCollider))]
    public class Controller : MonoBehaviour
    {

        public Character Character = null;
        public LayerMask StaticLayer = 0;

        [HideInInspector] public Vector3 normalOfGround = Vector3.zero;  // �����Ǵ� ����� �븻 ����
        [HideInInspector] public float pointAngle = 0.0f;           // ���� ������ ����
        [HideInInspector] public bool Movable = false;              // �̵� ���� ����
        [HideInInspector] public bool Hitted = false;               // ĸ�� �ݶ��̴��� �浹����

        [Header("Movable Setting")]
        public float MaxSlopeAngle = 55.0f;
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

        protected void OnCollisionStay(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
            {
                ContactPoint groundPoint = collision.contacts[0];

                normalOfGround = groundPoint.normal;    // �����Ǵ� ��ġ�� ���� ����
                CheckMovable(normalOfGround);

                // ���� ������ ���
                if (normalOfGround == Vector3.up)
                    pointAngle = 0.0f;

                Hitted = true;
            }
        }

        private void CheckMovable(Vector3 normal)
        {
            // ������ �������� ������ �κ��� ���� ���ϱ�
            float dot = Vector3.Dot(normal, transform.up);
            pointAngle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            if (Mathf.Abs(pointAngle) < MaxSlopeAngle)
                Movable = true;
            else
                Movable = false;
        }

        protected void OnCollisionExit(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
            {
                Hitted = false;
            }
        }
    }
}

