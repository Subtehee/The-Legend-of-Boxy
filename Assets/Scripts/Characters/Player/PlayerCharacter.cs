// ============================
// ���� : 2021-06-23
// �ۼ� : sujeong
// ============================

using System.Collections;
using System;
using UnityEngine;
using Characters.FSM;
using Characters.FSM.States;
using Characters.Stat;

namespace Characters.Player
{
    // �÷��̾��� ���� ������
    [System.Serializable]
    public enum States
    {
        IDLE,
        RUN,
        SPRINT,
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
        [SerializeField] private PlayerStat Stat = null;     // Player Setting

        [Header("Movement State")]
        public States State = States.IDLE;      // init State

        public Transform playerDirection = null;
        private Vector2 moveDirection = Vector2.zero;

        private bool IsGrounded = false;        // ���� ��� �ִ���
        private bool Climbable = false;         // ������ ���� �� �ִ� ��������

        protected override void Awake()
        {
            base.Awake();

            rigid = GetComponent<Rigidbody>();
            anim = GetComponent<Animator>();
            Controller ??= GetComponent<PlayerController>();
            Stat ??= FindObjectOfType<PlayerStat>();

            // Actions
            var idle = new PlayerAction_Idle(this, rigid, anim, Stat.GroundedGravity, Stat.Acceleration);
            var run = new PlayerAction_Move(this, rigid, anim, Stat.GroundedGravity, Stat.RunSpeed, Stat.SprintSpeed, Stat.Acceleration);

            // Add Transitions
            AddTransition(idle, run, CanMove);
            AddTransition(run, idle, CantMove);

            void AddTransition(IState from, IState to, Func<bool> condition) => FSM.AddTransition(from, to, condition);

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

            playerDirection = Controller.PlayerCamera.transform;
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
            Controller.FixedUpdateControl();
        }

        // THINK : ���� �������� ����� ���� �׼� ��ũ��Ʈ���� ����� �� �ֵ���...

        public void Rotate()
        {

        }

        public void Move()
        {

        }

        private Vector3 GetMoveDirection(Vector2 moveInput)
        {
            Transform direction = Controller.GetPlayerDirection();

            return (direction.forward * moveInput.y + direction.right * moveInput.x).normalized;
        }


    }

}
