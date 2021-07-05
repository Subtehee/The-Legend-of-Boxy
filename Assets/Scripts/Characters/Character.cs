// ============================
// ���� : 2021-07-05
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
        CLIMBUP,        // 8
        CLIMBDROP,      // 9
        CLIMBMOVE,      // 10
        CLIMBTOP,       // 11
        GLIDE,          
        AUTOATTACK,     
        STRONGATTACK,   
        SKILLATTACK,    
        DIE             
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
        [HideInInspector] public float distanceFromGround = 0.0f;

        protected Animator m_animator = null;
        protected FiniteStateMachine FSM = null;

        protected float m_smoothVelocity = 0.0f;        // Restore SmoothRotate Velocity 
        protected float m_animtaionDelay = 0.0f;

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
        }

        protected virtual void LateUpdate()
        {
            // ����ĳ���� w
            Controller.LateUpdateControl();
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
            if (Controller.Movable && Mathf.Abs(Controller.surfaceAngle) > float.Epsilon)
            {
                _moveDirection = GetDirectionSlope(_moveDirection, Controller.normalOfGround);
            }

            Vector3 targetVelocity = _moveDirection * targetSpeed;
            curSpeed = targetSpeed;

            m_rigidbody.velocity = targetVelocity;
        }

        protected Vector3 GetDirectionSlope(Vector3 direction, Vector3 stanNormal)
        {
            // ������ ���⿡ ���� ������ ���� ����
            Vector3 prpDir = Vector3.Cross(Vector3.up, direction).normalized;
            Vector3 planeDir = Vector3.ProjectOnPlane(stanNormal, prpDir);   // ����(surface)�� ������ ����
            direction = Vector3.Cross(prpDir, planeDir);

            //_moveDirection = (prpDir * moveDirection.magnitude) / Mathf.Sqrt(_moveDirection.x * _moveDirection.x + _moveDirection.z * _moveDirection.z);

            return direction;
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
