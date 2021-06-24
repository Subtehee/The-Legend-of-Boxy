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
        [Header("User Settings")]
        public readonly float RotateSpeed = 0.01f; // 회전 속도

        

        [Header("Player Const Status")]
        public readonly float MaxStamina = 300.0f;       // 최대 스태미나
        public readonly float MaxHP = 500.0f;            // 최대 체력
        public readonly float MaxDamage = 300.0f;        // 최대 데미지

    }
}

