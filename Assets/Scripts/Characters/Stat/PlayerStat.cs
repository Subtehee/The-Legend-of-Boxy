// ============================
// ���� : 2021-06-24
// �ۼ� : sujeong
// ============================

using UnityEngine;

namespace Characters.Stat
{

    [CreateAssetMenu(fileName ="PlayerStat", menuName ="Stat/PlayerStat")]
    public class PlayerStat : ScriptableObject
    {
        [Header("User Settings")]
        public readonly float RotateSpeed = 0.01f; // ȸ�� �ӵ�

        

        [Header("Player Const Status")]
        public readonly float MaxStamina = 300.0f;       // �ִ� ���¹̳�
        public readonly float MaxHP = 500.0f;            // �ִ� ü��
        public readonly float MaxDamage = 300.0f;        // �ִ� ������

    }
}

