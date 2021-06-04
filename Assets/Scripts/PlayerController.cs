/* 20210604_
 * 
 */

using UnityEngine;

namespace PlayerCharacter
{
    [CreateAssetMenu(fileName ="PlayerController", menuName ="Player/PlayerController")]
    public class PlayerController : Controller
    {
        public float ControlRotationSensitvity = 3.0f;      // 카메라 컨트롤 감도

        private PlayerInput _playerInput = null;
        private PlayerCamera _playerCamera = null;

        // 초기화
        public override void Init()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _playerCamera = FindObjectOfType<PlayerCamera>();

        }

        // 매 프레임마다 실행
        public override void OnCharacterUpdate()
        {
            // 입력값 갱신
            _playerInput.UpdateInput();     

        }

        // FixedUpdate마다 실행
        public override void OnCharacterFixedUpdate()
        {

        }


        // 카메라 회전 컨트롤 설정
        private void UpdateControlRotation()
        {
            Vector2 camInput = _playerInput.CameraInput;

        }

    }

}
