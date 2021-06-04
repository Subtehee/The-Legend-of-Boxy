/* 20210604_
 * 
 */

using UnityEngine;

namespace PlayerCharacter
{
    [CreateAssetMenu(fileName ="PlayerController", menuName ="Player/PlayerController")]
    public class PlayerController : Controller
    {
        public float ControlRotationSensitvity = 3.0f;      // ī�޶� ��Ʈ�� ����

        private PlayerInput _playerInput = null;
        private PlayerCamera _playerCamera = null;

        // �ʱ�ȭ
        public override void Init()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _playerCamera = FindObjectOfType<PlayerCamera>();

        }

        // �� �����Ӹ��� ����
        public override void OnCharacterUpdate()
        {
            // �Է°� ����
            _playerInput.UpdateInput();     

        }

        // FixedUpdate���� ����
        public override void OnCharacterFixedUpdate()
        {

        }


        // ī�޶� ȸ�� ��Ʈ�� ����
        private void UpdateControlRotation()
        {
            Vector2 camInput = _playerInput.CameraInput;

        }

    }

}
