// ============================
// ���� : 2021-06-13
// �ۼ� : sujeong
// ============================

using UnityEngine;

namespace Characters.Player
{
    public class PlayerController : MonoBehaviour
    {
        public Transform player = null;
        public PlayerInput PlayerInput = null;
        public PlayerCamera PlayerCamera = null;

        [Header("UserSetting")]
        [Range(1, 5)]public float cameraSensitivity = 3.0f;

        private void Awake()
        {
            if (player == null)
                player = transform;
        }

        public void UpdateControl()
        {
            PlayerInput.UpdateInputs();
        }

        public void FixedUpdateControl()
        {
            PlayerCamera.SetCameraPosition(player.position);
            PlayerCamera.SetCameraRotation(GetTargetRotation());
        }

        // �Է°��� ���� ī�޶��� �̵� �Ÿ� �����ϱ�
        private Vector2 GetTargetRotation()
        {
            Vector2 rotation = Vector2.zero;

            if (PlayerInput.HasCameraInput)
            {
                rotation.x = PlayerInput.CameraInput.x * cameraSensitivity;   // yaw
                rotation.y = PlayerInput.CameraInput.y * cameraSensitivity;   // pitch
            }

            return rotation;
        }

        // �Է°��� ���� �ӵ� ����
    }

}

