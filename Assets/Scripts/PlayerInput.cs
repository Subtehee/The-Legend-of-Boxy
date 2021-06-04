/* 20210604_변수 추가, UpdateInput(), SetDashInput()
 * 
 */

using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerInput : MonoBehaviour
    {
        
        [SerializeField] private float moveAxisDeadZone = 0.2f;     // 제외할 Axis값의 범위
        [SerializeField] private float dashDelay = 1.5f;            // 대쉬 후 키 딜레이 타임

        public Vector2 MoveInput { get; private set; }      // 평지 이동 키입력
        public Vector2 LastMoveInput { get; private set; }  // 마지막으로 입력된 키입력
        public Vector2 CameraInput { get; private set; }    // 카메라 컨트롤 입력
        public bool JumpInput { get; private set; }         // 점프키 입력
        public bool DashInput { get; private set; }         // 대시키 입력
        public bool HasMoveInput { get; private set; }      // 키입력을 받았는지 여부


        // 매프레임마다 실행, 사용자 키입력값 갱신
        public void UpdateInput()
        {
            // 대시중이면 키입력을 받지 않음
            if (DashInput)
                return;
            

            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // 키 입력값이 DeadZone안이면 입력값 0으로 초기화
            if (Mathf.Abs(moveInput.x) < moveAxisDeadZone)
                moveInput.x = 0.0f;
            if (Mathf.Abs(moveInput.y) < moveAxisDeadZone)
                moveInput.y = 0.0f;

            // moveInput값이 0이상이면 true
            bool hasMoveInput = moveInput.sqrMagnitude > 0.0f;

            // 키입력을 가진 상태에서 새로운 키입력이 들어오지 않았다면 가지고 있던 값이 마지막 입력값이 됨
            if (HasMoveInput && !hasMoveInput)
                LastMoveInput = moveInput;

            MoveInput = moveInput;          // MoveInput 업데이트
            HasMoveInput = hasMoveInput;    // 키를 입력 받았는지 true / false

            CameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse y"));

            JumpInput = Input.GetButton("Jump");

            // 점프 중이 아닐 때만 대쉬 버튼 입력받기
            if (!JumpInput)
                DashInput = Input.GetMouseButtonUp(1);
            // Dash 키가 입력되면 일정 시간 딜레이주기
            if (DashInput)
                Invoke("SetDashInput", dashDelay);
        }

        // Dash가 끝난 후 DashInput값 되돌리기
        private void SetDashInput()
        {
            DashInput = false;
        }
    }
}

