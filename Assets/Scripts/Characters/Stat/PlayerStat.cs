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

        [Header("Movement Settings")]
        public float CurMoveSpeed = 0.0f;       // ���� �ӵ�
        public float RunSpeed = 4.0f;           // �޸���
        public float SprintSpeed = 7.0f;       // ��������
        public float Acceleration = 30.0f;      // ����
        public float Deceleration = 50.0f;      // ����
        public float TurnSpeed = 0.01f;         // ȸ�� �ӵ�
        public float JumpForce = 8.0f;         // ����

        [Header("Gravity Settings")]
        public float GroundedGravity = 5.0f;   // ���� ����
        public float AirborneGravity = 20.0f;  // ���� ����
        public float GlidingGravity = 10.0f;   // �۶��̵�
        public float MaxFallGravity = 40.0f;   // �ִ� ���� �ӵ�

        [Header("Player Const Status")]
        public float MaxStamina = 300.0f;  // �ִ� ���¹̳�
        public float MaxHP = 500.0f;       // �ִ� ü��
        public float MaxDamage = 300.0f;   // �ִ� ������

    }
}

