// ============================
// ���� : 2021-06-14
// �ۼ� : sujeong
// ============================

using System.Collections;
using UnityEngine;


namespace Characters.Player
{
    //[RequireComponent(typeof(Animator))]
    //[RequireComponent(typeof(Rigidbody))]
    public class PlayerCharacter : MonoBehaviour
    {
        public PlayerController PlayerController = null;

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

        private Rigidbody rigid = null;
        private Vector3 moveDirection = Vector3.zero;
        
        private void Awake()
        {
            PlayerController = GetComponent<PlayerController>();
            rigid = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            PlayerController.UpdateControl();

            // �÷��̾� �����̱�
        }

        private void FixedUpdate()
        {
            PlayerController.FixedUpdateControl();
            moveDirection = PlayerController.GetMoveDirection(); 
        }

        // Ű�ٿ� Ȯ��


    }

}
