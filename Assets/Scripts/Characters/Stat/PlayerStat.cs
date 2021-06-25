// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.Stat
{

    [CreateAssetMenu(fileName ="PlayerStat", menuName ="Stat/PlayerStat")]
    public class PlayerStat : ScriptableObject
    {

        [Header("Movement Settings")]
        public float CurMoveSpeed = 0.0f;       // 현재 속도
        public float RunSpeed = 8.0f;           // 달리기
        public float SprintSpeed = 15.0f;       // 전력질주
        public float Acceleration = 40.0f;      // 가속
        public float Deceleration = 40.0f;      // 감속
        public float TurnSpeed = 0.01f;         // 회전 속도
        public float JumpForce = 20.0f;         // 점프

        [Header("Gravity Settings")]
        public float GroundedGravity = 5.0f;   // 접지 상태
        public float AirborneGravity = 20.0f;  // 공중 낙하
        public float GlidingGravity = 10.0f;   // 글라이딩
        public float MaxFallGravity = 40.0f;   // 최대 낙하 속도

        [Header("Player Const Status")]
        public readonly float MaxStamina = 300.0f;  // 최대 스태미나
        public readonly float MaxHP = 500.0f;       // 최대 체력
        public readonly float MaxDamage = 300.0f;   // 최대 데미지

    }
}

