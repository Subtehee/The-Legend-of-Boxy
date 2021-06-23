// ============================
// 수정 : 2021-06-23
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.Stat
{

    [CreateAssetMenu(fileName ="PlayerStat", menuName ="Stat/PlayerStat")]
    public class PlayerStat : ScriptableObject
    {

        [Header("Movement Setting")]
        public float CurMoveSpeed = 0.0f;   // 현재 속도
        public float RunSpeed = 8.0f;       // 달리기
        public float SprintSpeed = 15.0f;   // 전력질주
        public float Acceleration = 40.0f;  // 엑셀
        public float TurnSpeed = 3.0f;      // 회전 속도
        public float JumpForce = 5.0f;      // 점프

        [Header("Gravity Setting")]
        public float GroundedGravity = 5.0f;    // 접지 상태
        public float AirborneGravity = 20.0f;   // 공중 낙하
        public float GlidingGravity = 10.0f;    // 글라이딩
        public float MaxFallGravity = 40.0f;    // 최대 낙하 속도

    }
}

