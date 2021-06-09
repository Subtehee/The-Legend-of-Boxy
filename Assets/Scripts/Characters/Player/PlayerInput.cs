using UnityEngine;

namespace Characters.Player
{

    public class PlayerInput : MonoBehaviour
    {

        public readonly float moveCameraAxisDeadZone = 0.2f;

        public Vector2 MoveInput { get; private set; }
        public Vector2 CameraInput { get; private set; }
        public Vector2 PreCameraInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool DashInput { get; private set; }
        public bool SprintInput { get; private set; }
        public bool AutoAttack { get; private set; }
        public bool SkillAttack { get; private set; }

        public bool HasMoveInput { get; private set; }

        private bool HasCameraInput = false;

        public void UpdateInputs()
        {

            MoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            HasMoveInput = MoveInput.magnitude > 0.0f;
            
            // Remove camera inputs inside deadzone
            if (Mathf.Abs(CameraInput.x) < moveCameraAxisDeadZone)
                cameraInput.x = 0.0f;
            if (Mathf.Abs(CameraInput.y) < moveCameraAxisDeadZone)
                cameraInput.y = 0.0f;

            if (cameraInput.magnitude > 0.0f)
                CameraInput = cameraInput;
            
            if(!HasMoveInput && !HasCameraInput)

            JumpInput = Input.GetButton("Jump");

            DashInput = Input.GetMouseButtonUp(1);

            AutoAttack = Input.GetMouseButtonUp(0);

            SkillAttack = Input.GetKeyUp(KeyCode.E);

        }
    }
}

