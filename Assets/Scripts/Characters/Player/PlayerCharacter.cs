// ============================
// ���� : 2021-06-20
// �ۼ� : sujeong
// ============================

using System.Collections;
using System;
using UnityEngine;
using Characters.FSM;
using Characters.FSM.States;

namespace Characters.Player
{
    // �÷��̾��� ���� ������
    [System.Serializable]
    public enum States
    {
        IDLE,
        MOVE,
        DASH,
        JUMP,
        CLIMB,
        FALL,
        DOWNFALL,
        GLIDE,
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
        public float CurMoveSpeed = 0.0f;   // ���� �ӵ�
        public float RunSpeed = 8.0f;      // �޸���
        public float SprintSpeed = 20.0f;   // ��������
        public float Acceleration = 40.0f;  // ����
        public float TurnSpeed = 3.0f;      // ȸ�� �ӵ�
        public float JumpForce = 5.0f;      // ����

        [Header("Gravity Setting")]
        public float GroundedGravity = 5.0f;    // ���� ����
        public float AirborneGravity = 20.0f;   // ���� ����
        public float GlidingGravity = 10.0f;    // �۶��̵�
        public float MaxFallGravity = 40.0f;    // �ִ� ���� �ӵ�

        [HideInInspector] public Transform playerDirection = null;
        [HideInInspector] public Vector2 moveDirection = Vector2.zero;

        private bool IsGrounded = false;        // ���� ��� �ִ���
        private bool Climbable = false;         // ������ ���� �� �ִ� ��������

        protected override void Awake()
        {
            base.Awake();

            rigid = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            Controller ??= GetComponent<PlayerController>();

            // Actions
            var idle = new PlayerAction_Idle(this, anim, GroundedGravity);
            var move = new PlayerAction_Move(this, rigid, anim, GroundedGravity, RunSpeed, SprintSpeed, Acceleration);

            // Add Transitions
            At(idle, move, CanMove);
            At(move, idle, CantMove);

            void At(IState from, IState to, Func<bool> condition) => FSM.AddTransition(from, to, condition);

            FSM.SetState(idle);     // init state

            bool CanMove() => InputManager.Instance.HasMoveInput;
            bool CantMove() => !CanMove();

            // �ൿ ���� (From)
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

        }

        private void Start()
        {
            rigid.freezeRotation = true;
        }

        protected override void Update()
        {
            base.Update();
            Controller.UpdateControl();

            playerDirection = Controller.GetPlayerDirection();

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Controller.FixedUpdateControl();
        }

        // ���� ���
    }

}
