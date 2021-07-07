// ============================
// ���� : 2021-07-05
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

        [HideInInspector] public Vector2 moveDirection = Vector2.zero;      // ������ ������ ����
        [HideInInspector] public Vector3 normalOfGround = Vector3.zero;     // �����Ǵ� ������ �븻 ����
        [HideInInspector] public float surfaceAngle = 0.0f;     // ���� ������ ����
        [HideInInspector] public bool Hitted = false;           // ĸ�� �ݶ��̴��� �浹����
        /*[HideInInspector]*/
        public bool Movable = false;

        [Header("Movable Setting")]
        [SerializeField] protected float SlopeLimit = 55.0f;
        [SerializeField] protected float MaxRayDistance = 5.0f;
        [SerializeField] protected float RayToGroundOffset = 0.1f;

        protected float m_angleOfGround = 0.0f;

        protected virtual void Awake()
        {
            if (StaticLayer == 0)
                StaticLayer = LayerMask.NameToLayer("STATICMESH");
        }

        public virtual void UpdateControl() { }
        public virtual void LateUpdateControl() { }
        public virtual void FixedUpdateControl() { }
        public virtual Vector3 GetMoveDirection() { return moveDirection; }

        protected virtual void OnCollisionStay(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
            {
                Hitted = true;
            }
        }

        protected void OnCollisionExit(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
            {
                Hitted = false;
            }
        }

        public float GetDistanceFromGround()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up * RayToGroundOffset, -transform.up, out hit, MaxRayDistance, StaticLayer))
            {
                normalOfGround = hit.normal;
                surfaceAngle = MeasureAngle(normalOfGround, Vector3.up);

                // ������ �� �ִ� �������� üũ
                if (surfaceAngle < SlopeLimit)
                    Movable = true;
                else
                    Movable = false;

                // ������� �Ÿ� ����
                if (hit.distance <= RayToGroundOffset)
                    return 0.0f;
                return hit.distance - RayToGroundOffset;
            }
            return MaxRayDistance;
        }

        protected float MeasureAngle(Vector3 normal, Vector3 standNormal)
        {
            // ��� ���Ϳ� ������ �� ���ϱ�
            float dot = Vector3.Dot(normal, standNormal);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            return angle;
        }

        protected Vector3 CalculateDirectionSlope(Vector3 yawVector, Vector3 normal)
        {
            // ������ ���⿡ ���� ������ ���� ����
            Vector3 prpDir = Vector3.Cross(Vector3.up, yawVector).normalized;
            Vector3 planeDir = Vector3.ProjectOnPlane(normal, prpDir);   // ����(surface)�� ������ ����
            Vector3 targetDirection = Vector3.Cross(prpDir, planeDir);

            //_moveDirection = (prpDir * moveDirection.magnitude) / Mathf.Sqrt(_moveDirection.x * _moveDirection.x + _moveDirection.z * _moveDirection.z);

            return targetDirection;
        }
    }
}
