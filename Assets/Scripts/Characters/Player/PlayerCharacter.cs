// ============================
// 수정 : 2021-06-25
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

        [SerializeField] private PlayerStat Stat = null;        // Player Settings

        private Quaternion _direction = Quaternion.identity;    // for get rotation of UserCamera
        private Vector2 _moveInput = Vector2.zero;              // user input

        protected override void Awake()
        {
            base.Awake();

            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            Controller ??= GetComponent<PlayerController>();
            Stat ??= FindObjectOfType<PlayerStat>();

            // Actions
            var idle = new PlayerAction_Idle(this, States.IDLE, Stat.GroundedGravity, Stat.Deceleration);
            var run = new PlayerAction_Run(this, States.RUN, Stat.RunSpeed, Stat.GroundedGravity, Stat.TurnSpeed);
            var jump = new PlayerAction_Jump(this, States.JUMP, Stat.JumpForce, Stat.AirborneGravity);
            var landing = new PlayerAction_Landing(this, States.LANDING, Stat.AirborneGravity);

            // Add Transitions
            AddTransition(idle, run, CanMove);
            AddTransition(idle, jump, IsJumpInput);
            AddTransition(run, idle, CantMove);
            AddTransition(run, jump, IsJumpInput);
            AddTransition(jump, landing, IsGrounded);

            void AddTransition(IState from, IState to, Func<bool> condition) => FSM.AddTransition(from, to, condition);

            // Conditions
            bool CanMove() => InputManager.Instance.HasMoveInput;
            bool CantMove() => !CanMove();
            bool IsJumpInput() => InputManager.Instance.JumpInput;
            bool IsGrounded() => this.IsGrounded;

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

            Debug.Log(IsGrounded);
            Debug.Log(m_rigidbody.velocity);
        }

        // Behaviors
        public override void UpdateMoveDirection()
        {
            _moveInput = InputManager.Instance.MoveInput;
            _direction = Controller.GetMoveDirection();

            moveDirection = ((_direction * Vector3.forward * _moveInput.y) 
                            + (_direction * Vector3.right * _moveInput.x)).normalized;
        }

        public override void OnRotate(float rotSpeed)
        {
            float targetAngle = Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg + _direction.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_smoothVelocity, rotSpeed);

            transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }

        public override void OnMove(float moveSpeed)
        {
            float targetSpeed = Mathf.Lerp(curSpeed, moveSpeed, Stat.Acceleration * Time.fixedDeltaTime);
            Vector3 targetVelocity = moveDirection * targetSpeed;
            curSpeed = targetSpeed;

            //m_rigidbody.velocity = new Vector3(targetVelocity.x, m_rigidbody.velocity.y, targetVelocity.z);

            m_rigidbody.AddForce(targetVelocity, ForceMode.VelocityChange);
            m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, moveSpeed);
        }

        public override void OnDecel()
        {
            m_rigidbody.velocity = Vector3.MoveTowards(m_rigidbody.velocity,
                new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f), Stat.Deceleration * Time.fixedDeltaTime);
        }

        public override void OnGravity(float gravity)
        {
            m_rigidbody.AddForce(-transform.up * gravity);

            if (m_rigidbody.velocity.y < -gravity)
                m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, -gravity, m_rigidbody.velocity.z);
        }
    }
}
