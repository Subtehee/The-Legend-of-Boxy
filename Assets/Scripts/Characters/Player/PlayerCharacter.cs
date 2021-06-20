// ============================
// ���� : 2021-06-20
// �ۼ� : sujeong
// ============================

using System.Collections;
using UnityEngine;
using Characters.FSM;

namespace Characters.Player
{
    // �÷��̾��� ���� ������
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
        public float CurMoveSpeed = 0.0f;       // ���� �ӵ�
        public float RunSpeed = 10.0f;          // �޸���
        public float SprintSpeed = 20.0f;       // ��������
        public float Acceleration = 30.0f;      // ���� --> Dash
        public float TurnSpeed = 3.0f;          // ȸ�� �ӵ�
        public float JumpForce = 5.0f;          // ����

        [Header("Gravity Setting")]
        public float GroundedGravity = 5.0f;    // ���� ����
        public float AirborneGravity = 20.0f;   // ���� ����
        public float GlidingGravity = 10.0f;    // �۶��̵�

        private bool IsGrounded = false;        // ���� ��� �ִ���
        private bool Climbable = false;         // ������ ���� �� �ִ� ��������

        protected override void Awake()
        {
            base.Awake();

            // �ൿ ����
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

        // ���� ���
    }

}
