using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Characters.Player
{
    [CreateAssetMenu(fileName ="PlayerController", menuName ="Controller/PlayerController")]
    public class PlayerController : ScriptableObject
    {
        
        public PlayerInput _playerInput = null;
        public PlayerCamera _playerCamera = null;
        public Vector2 m_cameraRotation = Vector2.zero;

        private float m_curSpeed = 0.0f;

        public void Init()
        {
            if (_playerInput == null)
                _playerInput = FindObjectOfType<PlayerInput>();
            if (_playerCamera == null)
                _playerCamera = FindObjectOfType<PlayerCamera>();

        }

        public void UpdateController()
        {
            _playerInput.UpdateInputs();
            UpdateCameraRotation();
        }

        public void FixedUpdateController(Vector3 targetPos)
        {
            _playerCamera.SetTargetPosition(targetPos);
            _playerCamera.SetCameraRotation(m_cameraRotation);
            _playerCamera.UpdateSocketPosition();

        }

        private void UpdateCameraRotation()
        {
            Vector2 currentInput = _playerInput.CameraInput;
            Vector2 preInput = _playerInput.PreCameraInput;

            // Adjust the Camera Rotation
            Vector2 controlRotation = (currentInput - preInput) * Stat.cameraSensitivity;

            m_cameraRotation = SetControlRotation(controlRotation);
            SetRotationSpeed(_playerInput.CameraInput);
        }

        // 
        private Vector2 SetControlRotation(Vector2 controlRotation)
        {
            // Adjust the pitch angle (X Rotation)
            float pitchAngle = controlRotation.y;
            pitchAngle %= 360.0f;
            pitchAngle = Mathf.Clamp(pitchAngle, Stat.minPitchAngle, Stat.maxPitchAngle);

            // Adjust the yaw angle (Y Rotation)
            float yawAngle = -controlRotation.x;
            yawAngle %= 360.0f;

            return new Vector2(pitchAngle, yawAngle);
        }

        // Set camera rotation speed
        private void SetRotationSpeed(Vector2 cameraInput)
        {
            // (Rotation Speed) 600 : 1200 => 0 : 1 
            float targetSpeed = cameraInput.magnitude * Stat.maxRotationSpeed;
            m_curSpeed = Mathf.MoveTowards(m_curSpeed, targetSpeed, Time.deltaTime);

            float rotationSpeed = Mathf.Lerp(Stat.maxRotationSpeed, Stat.minRotationSpeed, m_curSpeed / targetSpeed);
            _playerCamera.rotationSpeed = rotationSpeed;
        }
    }

}
