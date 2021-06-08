using UnityEngine;

namespace Characters.Player
{
    public class PlayerMoveInput : MonoBehaviour
    {

        public readonly float moveCameraAxisDeadZone = 0.2f;

        public Vector2 MoveInput { get; private set; }
        public Vector2 CameraInput { get; private set; }
        public Vector2 PreCameraInput { get; private set; }
        public bool HasCameraInput { get; private set; }
        public bool JumpInput { get; private set; }
        public bool DashInput { get; private set; }
        public bool SprintInput { get; private set; }

        public void UpdateInputs()
        {

            MoveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Remove camera inputs inside deadzone
            if (Mathf.Abs(CameraInput.x) < moveCameraAxisDeadZone)
                cameraInput.x = 0.0f;
            if (Mathf.Abs(CameraInput.y) < moveCameraAxisDeadZone)
                cameraInput.y = 0.0f;

            // Set cameraInput value
            if (cameraInput.magnitude > 0.0f)
            {
                CameraInput = cameraInput;
                HasCameraInput = true;
            }
            else HasCameraInput = false;

            JumpInput = Input.GetButton("Jump");

            DashInput = Input.GetMouseButtonUp(1);

        }
    }
}

