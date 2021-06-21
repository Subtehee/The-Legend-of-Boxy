// ============================
// 수정 : 2021-06-15
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.Player
{
    public class PlayerInput : MonoBehaviour
    {
        public readonly float moveCameraAxisDeadZone = 0.2f;
        public float sprintInputTime = 0.7f;    // not certain...

        public Vector2 MoveInput { get; private set; }  
        public Vector2 CameraInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool DashInput { get; private set; }
        public bool SprintInput { get; private set; }
        public bool AutoAttack { get; private set; }
        public bool SkillAttack { get; private set; }
        public bool HasMoveInput { get; private set; }
        public bool HasCameraInput { get; private set; }

        public void UpdateInputs()
        {

            MoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            HasMoveInput = MoveInput.magnitude > 0.0f;

            // Remove camera inputs inside deadzone
            if (Mathf.Abs(cameraInput.x) < moveCameraAxisDeadZone)
                cameraInput.x = 0.0f;
            if (Mathf.Abs(cameraInput.y) < moveCameraAxisDeadZone)
                cameraInput.y = 0.0f;

            if (cameraInput.sqrMagnitude > 0.0f)
            {
                CameraInput = cameraInput;
                HasCameraInput = true;
            }
            else HasCameraInput = false;

            JumpInput = Input.GetButtonUp("Jump");

            DashInput = Input.GetMouseButtonUp(1);

            AutoAttack = Input.GetMouseButtonUp(0);

            SkillAttack = Input.GetKeyUp(KeyCode.E);

        }
    }
}

