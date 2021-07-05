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

        [HideInInspector] public Vector3 normalOfGround = Vector3.zero;  // �����Ǵ� ����� �븻 ����
        [HideInInspector] public float surfaceAngle = 0.0f;     // ���� ������ ����
        [HideInInspector] public bool Hitted = false;           // ĸ�� �ݶ��̴��� �浹����
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

                // ������ �� �ִ� �������� üũ
                if (surfaceAngle < SlopeLimit)
                    Movable = true;
                else
                    Movable = false;
                
                // ������� �Ÿ� ����
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
            // ��� ���Ϳ� ������ �� ���ϱ�
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
