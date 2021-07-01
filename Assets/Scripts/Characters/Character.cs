// ============================
// ���� : 2021-07-01
// �ۼ� : sujeong
// ============================

using System.Collections;
using UnityEngine;
using Characters.FSM;
using Characters.FSM.Actions;

namespace Characters
{
    // ĳ������ ���� ������
    [System.Serializable]
    public enum States
    {
        IDLE,           // 0
        RUN,            // 1
        SPRINT,         // 2
        DASH,           // 3
        JUMP,           // 4
        LANDING,        // 5
        FALL,           // 6
        DOWNFALL,       // 7
        CLIMB,          // 8
        GLIDE,          // 9
        AUTOATTACK,     // 10
        STRONGATTACK,   // 11
        SKILLATTACK,    // 12
        DIE             // 13
    }

    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        public Controller Controller = null;
        public LayerMask staticLayer = 0;

        [Header("Movement State")]
        public States State = States.IDLE;

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;
        [HideInInspector] public Rigidbody m_rigidbody = null;
        [HideInInspector] public float curSpeed = 0.0f;

        protected Animator m_animator = null;
        protected FiniteStateMachine FSM = null;

        protected float m_smoothVelocity = 0.0f;        // Restore SmoothRotate Velocity 
        protected float m_distanceFromGround = 0.0f;
        protected float m_animtaionDelay = 0.0f;
        protected float m_slopeRayOffset = 0.1f;
        protected bool Climbable = false;

        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();
            Controller ??= GetComponent<Controller>();

            if (staticLayer == 0)
                staticLayer = LayerMask.NameToLayer("STATICMESH");

            Controller.Character = this;
        }

        protected virtual void Update()
        {
            FSM.UpdateState();
            Debug.DrawRay(Controller.groundPos - moveDirection.normalized * m_slopeRayOffset, moveDirection, Color.blue);
        }

        protected virtual void LateUpdate()
        {
            m_distanceFromGround = Controller.GetDistanceFromGround();
        }

        protected virtual void FixedUpdate()
        {
            FSM.FixedUpdateState();
        }


        // override Behaviors //
        // ���¿� ���� �޶����� �ൿ�� �Ű������� ����
        public virtual void UpdateMoveDirection() { }
        public virtual void OnRotate(float rotSpeed) { }
        public virtual void OnGravity(float gravity) { }
        public virtual void OnDecel() { }

        // Common Behaviors //
        public void OnMove(float moveSpeed, float accel)
        {
            float targetSpeed = Mathf.Lerp(curSpeed, moveSpeed, accel * Time.deltaTime);
            Vector3 _moveDirection = moveDirection.normalized;

            // �̵� ������ ���¿��� ������ �ƴ� ���(��������, ��������) ������ ������ ���� �����ϱ� 
            if (Controller.Movable && Mathf.Abs(Controller.pointAngle) > float.Epsilon)
            {

                float angle = Controller.pointAngle;

                RaycastHit hit;
                if (Physics.Raycast(Controller.groundPos - _moveDirection.normalized * m_slopeRayOffset, 
                                    _moveDirection, out hit, 1.0f, staticLayer))
                {
                    // �ӵ��� ������ ����� �ݺ��

                    Debug.Log("Is Uphill");
                }
                else
                {
                    angle *= (-1.0f);
                    Debug.Log("Is Downhill");
                }

                Vector3 targetAngle = Vector3.Cross(_moveDirection, Vector3.up);     // moveDirection�� right���� (pitch�� ���� ��)
                targetAngle *= angle;

                // ������ ������ pitch ����
                _moveDirection = Quaternion.Euler(targetAngle) * _moveDirection;

                Debug.DrawLine(transform.position + Vector3.up * 1.0f, transform.position + _moveDirection + Vector3.up * 1.0f, Color.red);


                // �� ������ ���� �� �س� �ߵ� ex) X���� ���
                //moveDirection = Quaternion.Euler(angle, 0.0f, 0.0f) * moveDirection;

            }

            Vector3 targetVelocity = _moveDirection * targetSpeed;
            curSpeed = targetSpeed;

            m_rigidbody.velocity = targetVelocity;
        }

        public void AddImpulseForce(Vector3 direction, float force)
        {
            m_rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        // Animation Funcs //
        public void ToAnimaition(int value)
        {
            // �ߺ� ����
            if (m_animator.GetInteger("State") == value)
                return;

            m_animator.SetInteger("State", value);
        }

        public void SetAnimtaionDelay(float delayTime)
        {
            m_animtaionDelay = delayTime;
        }
    }
}
