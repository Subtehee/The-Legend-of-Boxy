// ============================
// 수정 : 2021-06-20
// 작성 : sujeong
// ============================

using System.Collections;
using System;
using UnityEngine;
using Characters.FSM;
using Characters.FSM.States;

namespace Characters.Player
{
    // 플레이어의 상태 열거형
    [System.Serializable]
    public enum States
    {
        IDLE,
        RUN,
        JUMP,
        CLIMB,
        FALL,
        DOWNFALL,
        GLIDE,
        SPRINT,
        AUTOATTACK,
        STRONGATTACK,
        SKILLATTACK,
        DIE
    }

    public class PlayerCharacter : Character
    {
        public PlayerController Controller = null;

        [Header("Movement State")]
        public States State = States.IDLE;      // init State

        [Header("Movement Setting")]
        public float CurMoveSpeed = 0.0f;       // 현재 속도
        public float RunSpeed = 10.0f;       // 달리기
        public float SprintSpeed = 20.0f;       // 전력질주
        public float Acceleration = 30.0f;      // 가속 --> Dash
        public float TurnSpeed = 3.0f;          // 회전 속도
        public float JumpForce = 5.0f;          // 점프

        [Header("Gravity Setting")]
        public float GroundedGravity = 5.0f;    // 접지 상태
        public float AirborneGravity = 20.0f;   // 공중 낙하
        public float GlidingGravity = 10.0f;    // 글라이딩

        [HideInInspector] public Transform playerDirection = null;
        [HideInInspector] public Vector2 moveDirection = Vector2.zero;

        private bool IsGrounded = false;        // 땅에 닿고 있는지
        private bool Climbable = false;         // 절벽을 오를 수 있는 상태인지

        protected override void Awake()
        {
            base.Awake();

            rigid = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();

            Controller ??= GetComponent<PlayerController>();

            var run = new PlayerAction_Run(this, rigid, anim, RunSpeed);
            var idle = new PlayerAction_Idle(this, anim);

            // Add Transitions
            At(idle, run, HasMoveInput);
            At(run, idle, HasNotMoveInput);

            void At(IState from, IState to, Func<bool> condition) => FSM.AddTransition(from, to, condition);

            FSM.SetState(idle);     // init state

            bool HasMoveInput() => Controller.PlayerInput.HasMoveInput;
            bool HasNotMoveInput() => !HasMoveInput();

            // 행동 조건 (From)
            //*IDLE (Init)
            //  - To : RUN          / Condition : MoveInput     / Delay : No
            //  - To : JUMP         / Condition : JumpInput     / Delay : No
            //  - To : ATTACK       / Condition : ATTACK INPUT  / Delay : No
            //  - To : HIT          / Condition : Get Damage    / Delay : No

            //*RUN
            //  - To : IDLE         / Condition : No Input              / Delay : Velocity Zero
            //  - To : JUMP         / Condition : JumpInput             / Delay : No
            //  - To : DASH         / Condition : DashInput             / Delay : No
            //  - To : CLIMB        / Condition : Climbable             / Delay : No
            //  - To : FALL         / Condition : !IsGrounded && Range  / Delay : No
            //  - To : DOWNFALL     / Condition : !IsGrounded && !Range / Delay : NO
            //  - To : ATTACK       / Condition : ATTACK INPUT          / Delay : No

            //*DASH
            //  - To : IDLE     / Condition : No Input          / Delay : Dash Motion Over
            //  - To : SPRINT   / Condition : Keep DashInput    / Delay : Dash Motion Over
            //  - To : JUMP     / Condition : JumpInput         / Delay : No
            //  - To : ATTACK   / Condition : ATTACK INPUT      / Delay : Dash Motion Over
            //  - To :

            // TODO : !!!!!

            //*SPRINT
            //  - From : DASH   / Condition : Stamina Exist && DashInput Exist && Not ButtonUp

            //*JUMP
            //  - From : IDLE   / Condition : JumpInput
            //  - From : RUN    / Condition : JumpInput
            //  - From : DASH   / Condition : JumpInput
            //  - From : SPRINT / Condition : JumpInput

            //*CLIMB
            //  - From : RUN    / Condition : Climbable
            //  - From : JUMP   / Condition : Climbable
            //  - From : GLIDE  / Condition : Climbable
            //  - From : ?? -->

            //*LANDING
            //  - From : JUMP   / Condition : IsGrounded
            //  - From : FALL   / Condition : IsGrounded
            //  - From : GLIDE  / Condition : IsGrounded
            //  - Form : FALL   / Condition : IsGrounded

            //*FALL                                          <Raycastiong Foot>
            //  - From : JUMP   / Condition : Not IsGrounded && Range in Fall
            //  - From : RUN    / Condition : Not IsGrounded && Range in Fall
            //  - From : DASH   / Condition : Not IsGrounded && Range in Fall
            //  - From : SPRINT / Condition : Not IsGrounded && Range in Fall

            //*DOWNFALL
            //  - From : Jump   / Condition : Not IsGrounded && Range in DownFall
            //  - From : RUN    / Condition : Not IsGrounded && Range in DownFall
            //  - From : DASH   / Condition : Not IsGrounded && Range in DownFall
            //  - From : SPRINT / Condition : Not IsGrounded && Range in DownFall
            //  - From : GLIDE  / Condition : Not IsGrounded && Range in DownFall && JumpInput

            //*GLIDE
            //  - From : FALL       / Condition : JumpInput && Not IsGrounded
            //  - From : DOWNFALL   / Condition : JumpInput
            //// - From : IDLE, RUN, SPRINT, JUMP / Condition : JumpInput && WindCollider ////

            //*HIT
            //  - From : IDLE   / Condition : Get Damage
            //  - From : RUN    / Condition : Get Damage

            //*HARDHIT
            //  - From : IDLE           / Condition : Get Strong Damage
            //  - From : RUN            / Condition : Get Strong Damage
            //  - From : SPRINT         / Condition : Get Strong Damage && InvincibleTime is Over (0.5f ~ 1.0f)
            //  - From : AUTOATTACK     / Condition : Get Strong Damage
            //  - From : STRONGATTACK   / Condition : Get Strong Damage

            //*DIE
            //  - From : HIT        / Condition : HP Empty
            //  - From : HARDHIT    / Condition : HP Empty
            //  - From : DOWNFALL   / Condition : IsGrounded

            // <Combat>
            //*AUTOATTACK
            //  - From : IDLE   / Condition : AttackInput && HasWeapon
            //  - From : RUN    / Condition : AttackInput && HasWeapon
            //  - From : SPRINT / Condition : AttackInput && HasWeapon

            //*STRONGATTACK
            //  - From : AUTOATTACK / Condition : AttackInput Exist && Not ButtonUp && HasWeapon

            //*SKILLATTACK
            //  - From : IDLE   / Condition : SkillInput && HasWeapon
            //  - From : RUN    / Condition : SkillInput && HasWeapon
            //  - From : SPRINT / Condition : SkillInput && HasWeapon

            //*Aiming
            //  - From : IDLE   / Condition : AttackInput && Not ButtonUp && HasBow
            //  - From : RUN    / Condition : AttackInput && Not ButtonUp && HasBow
            //  - From : SPRINT / Condition : AttackInput && Not ButtonUp && HasBow
        }

        protected override void Update()
        {
            base.Update();
            Controller.UpdateControl();

            playerDirection = Controller.GetPlayerDirection();
            moveDirection = Controller.GetMoveDirection();

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Controller.FixedUpdateControl();
        }

        // 조건 명시
    }

}
