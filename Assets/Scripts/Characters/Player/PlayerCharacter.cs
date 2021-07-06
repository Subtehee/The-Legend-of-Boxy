// ============================
// 수정 : 2021-07-05
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

        public bool Climbable = false;

        protected override void Awake()
        {
            base.Awake();

            m_rigidbody = GetComponent<Rigidbody>();
            m_animator = GetComponent<Animator>();
            Controller ??= GetComponent<PlayerController>();
            Stat ??= FindObjectOfType<PlayerStat>();

            // Actions
            var idle = new ActionIdle(this, States.IDLE, Stat.GroundedGravity, Stat.Deceleration);
            var run = new ActionRun(this, States.RUN, Stat.RunSpeed, Stat.SprintSpeed, Stat.GroundedGravity, Stat.TurnSpeed, Stat.Acceleration);
            var jump = new ActionJump(this, States.JUMP, Stat.JumpForce, Stat.AirborneGravity);
            var landing = new ActionLanding(this, States.LANDING, Stat.AirborneGravity);
            var falling = new ActionFall(this, States.FALL, Stat.AirborneGravity);
            //var climbUp = new ActionClimb_Up(this, States.CLIMB);
            //var climbTop = new ActionClimb_Top(this, States.CLIMBTOP);

            AddTransition(idle, run, CanMove);
            AddTransition(idle, jump, IsJumpInput);
            AddTransition(idle, falling, Falling);
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
            bool Falling() => distanceFromGround > 0.5f && !Controller.Hitted;
            bool DownFalling() => distanceFromGround > 5.0f && !Controller.Hitted;
            bool IsLanding() => distanceFromGround < 0.3f && m_rigidbody.velocity.y < float.Epsilon;
            bool AnimtaionOver() => m_animtaionDelay < 0.0f;

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

        public override void UpdateMoveDirection()
        {
            moveDirection = Controller.GetMoveDirection();
        }
    }
}
