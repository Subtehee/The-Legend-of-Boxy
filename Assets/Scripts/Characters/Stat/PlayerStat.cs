// ============================
// ���� : 2021-06-23
// �ۼ� : sujeong
// ============================

using UnityEngine;

namespace Characters.Stat
{

    [CreateAssetMenu(fileName ="PlayerStat", menuName ="Stat/PlayerStat")]
    public class PlayerStat : ScriptableObject
    {

        [Header("Movement Setting")]
        public float CurMoveSpeed = 0.0f;   // ���� �ӵ�
        public float RunSpeed = 8.0f;       // �޸���
        public float SprintSpeed = 15.0f;   // ��������
        public float Acceleration = 40.0f;  // ����
        public float TurnSpeed = 3.0f;      // ȸ�� �ӵ�
        public float JumpForce = 5.0f;      // ����

        [Header("Gravity Setting")]
        public float GroundedGravity = 5.0f;    // ���� ����
        public float AirborneGravity = 20.0f;   // ���� ����
        public float GlidingGravity = 10.0f;    // �۶��̵�
        public float MaxFallGravity = 40.0f;    // �ִ� ���� �ӵ�

    }
}

