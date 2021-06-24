// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using System;
using UnityEngine;
using Characters.Stat;
using Characters.FSM;
using Characters.FSM.Actions;

namespace Characters.Player
{
    
    public class PlayerCharacter : Character
    {
        public PlayerController Controller = null;
        [SerializeField] private PlayerStat Stat = null;    // Player Settings

        [Header("Movement Settings")]
        public float CurMoveSpeed = 0.0f;       // 현재 속도
        public float RunSpeed = 8.0f;           // 달리기
        public float SprintSpeed = 15.0f;       // 전력질주
        public float Acceleration = 40.0f;      // 가속
        public float Deceleration = 40.0f;      // 감속
        public float TurnSpeed = 3.0f;          // 회전 속도
        public float JumpForce = 5.0f;          // 점프

        [Header("Gravity Settings")]
        public float GroundedGravity = -5.0f;   // 접지 상태
        public float AirborneGravity = -20.0f;  // 공중 낙하
        public float GlidingGravity = -10.0f;   // 글라이딩
        public float MaxFallGravity = -40.0f;   // 최대 낙하 속도

        private Transform _direction = null;                // for get direction of UserCamera
        private Vector2 _moveInput = Vector2.zero;          // user input
        private Vector3 m_moveDirection = Vector3.zero;

        protected override void Awake()
        {
            base.Awake();

            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            Controller ??= GetComponent<PlayerController>();
            Stat ??= FindObjectOfType<PlayerStat>();

            // Actions
            var idle = new PlayerAction_Idle(this, States.IDLE);
            var run = new PlayerAction_Run(this, States.RUN);
            var jump = new PlayerAction_Jump(this, States.JUMP, JumpForce, AirborneGravity);
            var landing = new PlayerAction_Landing(this, States.LANDING);

            // Add Transitions
            AddTransition(idle, run, CanMove);
            AddTransition(idle, jump, IsJumpInput);
            AddTransition(run, idle, CantMove);
            AddTransition(run, jump, IsJumpInput);

            void AddTransition(IState from, IState to, Func<bool> condition) => FSM.AddTransition(from, to, condition);

            bool CanMove() => InputManager.Instance.HasMoveInput;
            bool CantMove() => !CanMove();
            bool IsJumpInput() => InputManager.Instance.JumpInput;

            // init state
            FSM.SetState(idle);     
        }

        private void Start()
        {
            m_rigidbody.freezeRotation = true;
            m_rigidbody.useGravity = false;
        }

        protected override void Update()
        {
            base.Update();
            Controller.UpdateControl();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Controller.FixedUpdateControl();
        }

        // 공통 움직임 //
        public override void UpdateMoveDirection()
        {
            _moveInput = InputManager.Instance.MoveInput;
            _direction = Controller.GetPlayerDirection();
            m_moveDirection = (_direction.forward * _moveInput.y + _direction.right * _moveInput.x).normalized;
        }

        public override void OnRotate()
        {
            float targetAngle = Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg + _direction.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_smoothVelocity, Stat.RotateSpeed);

            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }

        public override void OnMove(float moveSpeed)
        {
            // Velocity 
            float targetSpeed = Mathf.Lerp(curSpeed, moveSpeed, Acceleration * Time.fixedDeltaTime);

            m_rigidbody.velocity = m_moveDirection * targetSpeed;
            curSpeed = targetSpeed;
        }

        public override void OnDecel()
        {
            m_rigidbody.velocity = Vector3.MoveTowards(m_rigidbody.velocity,
                new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f), Deceleration * Time.fixedDeltaTime);
        }

        public override void OnGravity(float gravity)
        {
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, gravity, m_rigidbody.velocity.z);
        }
    }
}
