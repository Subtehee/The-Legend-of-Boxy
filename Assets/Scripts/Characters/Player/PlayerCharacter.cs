// ============================
// ���� : 2021-06-14
// �ۼ� : sujeong
// ============================

using System.Collections;
using UnityEngine;
using Characters.State;

namespace Characters.Player
{
    // �÷��̾��� ���� ������
    public enum MoveState
    {
        IDLE,
        RUN,
        JUMP,
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

        [SerializeField] private FiniteStateMachine<PlayerCharacter> FSM = null;
        //public MoveState State = MoveState.IDLE;

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


        protected override void Awake()
        {
            base.Awake();
            FSM ??= FindObjectOfType<FiniteStateMachine<PlayerCharacter>>();

            // �ൿ ���

        }

        protected override void Update()
        {
            base.Update();

            // �÷��̾� �����̱�
        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        // ���� ���


    }

}
