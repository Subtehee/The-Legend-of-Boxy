// ============================
// 수정 : 2021-06-24
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
        IDLE,
        RUN,
        SPRINT,
        DASH,
        JUMP,
        LANDING,
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
        [Header("Movement State")]
        public States State = States.IDLE;      

        [Header("ScriptableObject")]
        protected FiniteStateMachine FSM = null;
        public CharacterStat Stat = null;

        protected Rigidbody m_rigidbody = null;
        protected Animator m_animator = null;

        [HideInInspector] public float curSpeed = 0.0f;     // current Move Speed

        protected float m_smoothVelocity = 0.0f;    // Restore SmoothRotate Velocity 
        protected bool IsGrounded = false;
        protected bool Climbable = false;

        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();
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
        public virtual void OnGravity(float gravity) { }
        public virtual void OnMove(float moveSpeed) { }
        public virtual void OnRotate() { }
        public virtual void OnDecel() { }

        public void AddImpulseForce(Vector3 direction, float force) 
        {
            m_rigidbody.AddForce(direction * force);
        }

        public void ToAnimaition(int value)
        {
            m_animator.SetInteger("State", value);
        }
    }
}
