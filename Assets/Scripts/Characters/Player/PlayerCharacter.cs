// ============================
// 수정 : 2021-06-20
// 작성 : sujeong
// ============================

using System.Collections;
using UnityEngine;
using Characters.FSM;

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
        public States State = States.IDLE;      // init State

        [Header("Movement Setting")]
        public float CurMoveSpeed = 0.0f;       // 현재 속도
        public float RunSpeed = 10.0f;          // 달리기
        public float SprintSpeed = 20.0f;       // 전력질주
        public float Acceleration = 30.0f;      // 가속 --> Dash
        public float TurnSpeed = 3.0f;          // 회전 속도
        public float JumpForce = 5.0f;          // 점프

        [Header("Gravity Setting")]
        public float GroundedGravity = 5.0f;    // 접지 상태
        public float AirborneGravity = 20.0f;   // 공중 낙하
        public float GlidingGravity = 10.0f;    // 글라이딩

        private bool IsGrounded = false;        // 땅에 닿고 있는지
        private bool Climbable = false;         // 절벽을 오를 수 있는 상태인지

        protected override void Awake()
        {
            base.Awake();

            // 행동 조건
            // <Movement>
            //*IDLE
            //  - Init
            //  - From : Run        / Condition : No MoveInput
            //  - From : SPRINT     / Condition : No MoveInput
            //  - From : CLIMB      / Condition : Climb the Top <Raycasting Head>
            //  - From : LANDING    / Condition : AUTO
            //  - From : HIT        / Condition : AUTO
            //  - From : ALL ATTACK / Condition : AUTO

            //*RUN
            //  - From : IDLE       / Condition : MoveInput
            //  - From : DASH       / Condition : (DASH is Over) && MoveInput
            //  - From : SPRINT     / Condition : Stamina Empty || DashInput

            //*DASH
            //  - From : IDLE           / Condition : DashInput && Stamina Exist
            //  - From : RUN            / Condition : DashInput && Stamina Exist
            //  - From : LANDING        / Condition : DashInput && Stamina Exist --> No need IDLE
            //  - From : AUTOATTACK     / Condition : DashInput && Stamina Exist
            //  - From : STRONGATTACK   / Condition : DashInput && Stamina Exist

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
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        // 조건 명시
    }

}
