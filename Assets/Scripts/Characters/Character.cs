// ============================
// 수정 : 2021-07-05
// 작성 : sujeong
// ============================

using System.Collections;
using UnityEngine;
using Characters.FSM;
using Characters.FSM.Actions;

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
        public LayerMask StaticLayer = 0;

        [Header("Movement State")]
        public States State = States.IDLE;

        [Header("Movable Setting")]
        public float SlopeLimit = 55.0f;
        public float MaxRayDistance = 5.0f;
        public float MoveRayOffset = 0.1f;

        [HideInInspector] public Vector3 moveDirection = Vector3.zero;          // 움직일 방향
        [HideInInspector] public Vector2 targetDirection = Vector2.zero;        // 회전할 방향
        [HideInInspector] public Quaternion curRotation = Quaternion.identity;  // 현재의 회전 방향
        [HideInInspector] public Rigidbody rigid = null;

        protected Animator animator = null;
        protected FiniteStateMachine FSM = null;

        protected bool Movable = false;
        protected float m_smoothVelocity = 0.0f;        // Restore SmoothRotate Velocity 
        protected float m_animtaionDelay = 0.0f;
        protected float distanceFromGround = 0.0f;

        protected virtual void Awake()
        {
            FSM = new FiniteStateMachine();

            if (StaticLayer == 0)
                StaticLayer = LayerMask.NameToLayer("STATICMESH");

            Controller ??= GetComponent<Controller>();
            Controller.Character = this;
        }

        protected virtual void Update()
        {
            FSM.UpdateState();
        }

        protected virtual void LateUpdate() 
        {
            // 지면에서 떨어진 거리 측정
            distanceFromGround = Controller.GetDistanceFromGround();
        }

        protected virtual void FixedUpdate()
        {
            FSM.FixedUpdateState();
        }

        public void UpdateMoveDirection() 
        {
            moveDirection = Controller.GetMoveDirection();
        }

        // Animation Funcs //
        public void ToAnimaition(int value)
        {
            // 중복 리턴
            if (animator.GetInteger("State") == value)
                return;

            animator.SetInteger("State", value);
        }

        public void SetAnimtaionDelay(float delayTime)
        {
            m_animtaionDelay = delayTime;
        }
    }
}
