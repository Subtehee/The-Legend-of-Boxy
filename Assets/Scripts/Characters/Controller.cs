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

        [HideInInspector] public Vector3 normalOfGround = Vector3.zero;  // 접지되는 평면의 노말 벡터
        [HideInInspector] public float surfaceAngle = 0.0f;     // 접지 지점의 각도
        [HideInInspector] public bool Hitted = false;           // 캡슐 콜라이더의 충돌여부
        /*[HideInInspector]*/ public bool Movable = false;             

        [Header("Movable Setting")]
        public float SlopeLimit = 55.0f;
        public float MaxRayDistance = 5.0f;
        public float MoveRayOffset = 0.1f;

        protected Quaternion charQuaternion = Quaternion.identity;
        protected float m_angleOfGround = 0.0f;

        protected virtual void Awake()
        {
            if (StaticLayer == 0)
                StaticLayer = LayerMask.NameToLayer("STATICMESH");
        }

        public virtual void UpdateControl() { }         
        
        public virtual void LateUpdateControl() 
        {
            Character.distanceFromGround = GetDistanceFromGround();
        }   
        
        public virtual void FixedUpdateControl() { }    
        public virtual Quaternion GetMoveDirection() { return charQuaternion; }

        protected float GetDistanceFromGround()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position + transform.up * MoveRayOffset, -transform.up, out hit, MaxRayDistance, StaticLayer))
            {
                normalOfGround = hit.normal;
                surfaceAngle = MeasureAngle(normalOfGround, Vector3.up);

                // 움직일 수 있는 각도인지 체크
                if (surfaceAngle < SlopeLimit)
                    Movable = true;
                else
                    Movable = false;
                
                // 지면과의 거리 측정
                if (hit.distance <= MoveRayOffset)
                    return 0.0f;
                return hit.distance - MoveRayOffset;
            }
            return MaxRayDistance;
        }

        protected virtual void OnCollisionStay(Collision collision)
        {
            if (collision.transform.CompareTag("STATICMESH"))
            {
                Hitted = true;
            }
        }

        protected float MeasureAngle(Vector3 normal, Vector3 standNormal)
        {
            // 노멀 벡터와 지면의 각 구하기
            float dot = Vector3.Dot(normal, standNormal);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;

            return angle;
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
