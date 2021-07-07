// ============================
// 수정 : 2021-07-05
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

        [HideInInspector] public Vector2 moveDirection = Vector2.zero;      // 움직일 방향의 벡터
        [HideInInspector] public Vector3 normalOfGround = Vector3.zero;     // 접지되는 지면의 노말 벡터
        [HideInInspector] public float surfaceAngle = 0.0f;     // 접지 지점의 각도
        [HideInInspector] public bool Hitted = false;           // 캡슐 콜라이더의 충돌여부
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

                // 움직일 수 있는 각도인지 체크
                if (surfaceAngle < SlopeLimit)
                    Movable = true;
                else
                    Movable = false;

                // 지면과의 거리 측정
                if (hit.distance <= RayToGroundOffset)
                    return 0.0f;
                return hit.distance - RayToGroundOffset;
            }
            return MaxRayDistance;
        }

        protected float MeasureAngle(Vector3 normal, Vector3 standNormal)
        {
            // 노멀 벡터와 지면의 각 구하기
            float dot = Vector3.Dot(normal, standNormal);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            return angle;
        }

        protected Vector3 CalculateDirectionSlope(Vector3 yawVector, Vector3 normal)
        {
            // 지면의 기울기에 따라 움직일 방향 조정
            Vector3 prpDir = Vector3.Cross(Vector3.up, yawVector).normalized;
            Vector3 planeDir = Vector3.ProjectOnPlane(normal, prpDir);   // 지면(surface)과 평행한 벡터
            Vector3 targetDirection = Vector3.Cross(prpDir, planeDir);

            //_moveDirection = (prpDir * moveDirection.magnitude) / Mathf.Sqrt(_moveDirection.x * _moveDirection.x + _moveDirection.z * _moveDirection.z);

            return targetDirection;
        }
    }
}
