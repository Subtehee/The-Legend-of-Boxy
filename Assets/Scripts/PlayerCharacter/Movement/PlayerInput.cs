using UnityEngine;

namespace PlayerCharacter.Movement
{
    public class PlayerInput : MonoBehaviour
    {
        
        public float m_moveAxisDeadZone = 0.2f;    
        public float m_dashDelayTime = 1.5f;

        private Vector2 moveDirection = Vector2.zero;       // current move direction
        private Vector2 m_moveInput = Vector2.zero;         // keyboard input
        private Vector2 m_cameraInput = Vector2.zero;       // mouse scroll input
        private bool m_jumpInput = false;
        private bool m_attackInput = false;
        private bool m_dashInput = false;
        private bool m_sprintInput = false;

        public bool JumpInput { get { return m_jumpInput; } private set { } }
        public bool AttackInput { get { return m_attackInput; } private set { } }
        public bool DashInput { get { return m_dashInput; } private set { } }
        public bool SprintInput { get { return m_sprintInput; } private set { } }
        
        public void UpdateInputs()
        {
            // Get move direction inputs
            m_moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            m_cameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

            // Remove camera inputs inside deadzone
            if (Mathf.Abs(m_cameraInput.x) < m_moveAxisDeadZone)
                m_cameraInput.x = 0.0f;
            if (Mathf.Abs(m_cameraInput.y) < m_moveAxisDeadZone)
                m_cameraInput.y = 0.0f;

            // Set move direction by keyboard when mouse do not move
            if (m_cameraInput.magnitude > 0.0f)
                moveDirection = m_moveInput;

            // Get jump key input
            m_jumpInput = Input.GetButton("Jump");

            // Can dash if not jumped
            if (!m_jumpInput)
            {
                DashInput = Input.GetMouseButtonUp(1);

            }
        }
    }
}

