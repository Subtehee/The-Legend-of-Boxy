// ============================
// 수정 : 2021-06-28
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
            var run = new PlayerAction_Run(this, States.RUN, Stat.RunSpeed, Stat.SprintSpeed, Stat.GroundedGravity, Stat.TurnSpeed);
            var jump = new PlayerAction_Jump(this, States.JUMP, Stat.JumpForce, Stat.AirborneGravity);
            var landing = new PlayerAction_Landing(this, States.LANDING, Stat.AirborneGravity);
            var falling = new PlayerAction_Fall(this, States.FALL, Stat.AirborneGravity);

            AddTransition(idle, run, CanMove);
            AddTransition(idle, jump, IsJumpInput);
            AddTransition(run, idle, CantMove);
            AddTransition(run, jump, IsJumpInput);
            AddTransition(run, falling, Falling);
            AddTransition(jump, landing, IsLanding);
            AddTransition(jump, falling, Falling);
            AddTransition(landing, idle, AnimtaionOver);
            AddTransition(falling, landing, IsLanding);

            void AddTransition(IState from, IState to, Func<bool> condition) => FSM.AddTransition(from, to, condition);

            // Conditions
            bool CantMove() => !CanMove();
            bool CanMove() => InputManager.Instance.HasMoveInput;
            bool IsJumpInput() => InputManager.Instance.JumpInput;
            bool Falling() =>  m_distanceFromGround > 1.0f + 0.9f && m_rigidbody.velocity.y < float.Epsilon;
            bool DownFalling() => m_distanceFromGround > 2.0f + 0.9f && m_rigidbody.velocity.y < float.Epsilon;
            bool IsLanding() => m_distanceFromGround < 0.3f && m_rigidbody.velocity.y < float.Epsilon;
            bool AnimtaionOver() => m_animtaionDelay < float.Epsilon;

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

            // Animtaion Delay
            if (m_animtaionDelay > 0.0f)
                m_animtaionDelay -= Time.deltaTime;
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Controller.FixedUpdateControl();
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
            float targetSpeed = Mathf.Lerp(curSpeed, moveSpeed, Stat.Acceleration * Time.deltaTime);
            Vector3 targetVelocity = moveDirection * targetSpeed;
            curSpeed = targetSpeed;

            m_rigidbody.velocity = new Vector3(targetVelocity.x, m_rigidbody.velocity.y, targetVelocity.z);

            //m_rigidbody.AddForce(targetVelocity, ForceMode.VelocityChange);
            //m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, moveSpeed);
        }

        public override void OnDecel()
        {
            // velocity deceleration
            m_rigidbody.velocity = Vector3.MoveTowards(m_rigidbody.velocity,
                new Vector3(0.0f, m_rigidbody.velocity.y, 0.0f), Stat.Deceleration * Time.deltaTime);

            // gravity deceleration
            if (m_rigidbody.velocity.y > 0.0f)
                m_rigidbody.velocity = Vector3.MoveTowards(m_rigidbody.velocity,
                    new Vector3(m_rigidbody.velocity.x, 0.0f, m_rigidbody.velocity.z), Stat.Deceleration * Time.deltaTime);
        }

        public override void OnGravity(float gravity)
        {
            m_rigidbody.AddForce(-transform.up * gravity);

            if (m_rigidbody.velocity.y < -gravity)
                m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, -gravity, m_rigidbody.velocity.z);
        }
    }
}
