// ============================
// 수정 : 2021-06-14
// 작성 : sujeong
// ============================

using System.Collections;
using UnityEngine;


namespace Characters.Player
{
    public class PlayerCharacter : Character
    {

        // 캐릭터의 상태 FSM

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
        
        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Update()
        {
            base.Update();
            // 플레이어 움직이기

        }

        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        // 상태값에 따른 캐릭터 행동
    }

}
