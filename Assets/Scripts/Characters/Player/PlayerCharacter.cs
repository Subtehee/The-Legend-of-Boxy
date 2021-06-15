// ============================
// ���� : 2021-06-14
// �ۼ� : sujeong
// ============================

using System.Collections;
using UnityEngine;


namespace Characters.Player
{
    public class PlayerCharacter : Character
    {

        // ĳ������ ���� FSM

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

        // ���°��� ���� ĳ���� �ൿ
    }

}
