// ============================
// 수정 : 2021-06-14
// 작성 : sujeong
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

            // 플레이어 움직이기
        }

        private void FixedUpdate()
        {
            PlayerController.FixedUpdateControl();
            moveDirection = PlayerController.GetMoveDirection(); 
        }

        // 키다운 확인


    }

}
