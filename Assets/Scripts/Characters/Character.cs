// ============================
// 수정 : 2021-06-25
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.FSM;
using Characters.Stat;

namespace Characters
{
    // 캐릭터의 상태 열거형
    [System.Serializable]
    public enum States
    {
        IDLE,           // 0
        RUN,            // 1
        SPRINT,         // 2
        DASH,           // 3
        JUMP,           // 4
        LANDING,        // 5
        CLIMB,
        FALL,
        DOWNFALL,
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

        [Header("Movement State")]
        public States State = States.IDLE;      

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;     
        [HideInInspector] public float curSpeed = 0.0f; 
        [HideInInspector] public bool IsGrounded = false;

        protected Rigidbody m_rigidbody = null;
        protected FiniteStateMachine FSM = null;
        protected Animator m_animator = null;
        protected float m_smoothVelocity = 0.0f;    // Restore SmoothRotate Velocity 
        protected bool Climbable = false;

        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();
            Controller ??= GetComponent<Controller>();
            Controller.Character = this;
        }

        protected virtual void Update()
        {
            FSM.UpdateState();
        }

        protected virtual void FixedUpdate()
        {
            FSM.FixedUpdateState();
        }

        public virtual void UpdateMoveDirection() { }
        public void FixedUpdateIsGrounded()
        {
            IsGrounded = Controller.IsGrounded();
        }

        // Behaviors
        // 상태에 따라 달라지는 행동은 매개변수를 받음
        public virtual void OnGravity(float gravity) { }
        public virtual void OnMove(float moveSpeed) { }
        public virtual void OnRotate(float rotSpeed) { }
        public virtual void OnDecel() { }


        public void AddImpulseForce(Vector3 direction, float force) 
        {
            m_rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }

        public void ToAnimaition(int value)
        {
            m_animator.SetInteger("State", value);
        }
    }
}
