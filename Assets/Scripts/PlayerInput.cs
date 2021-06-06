using UnityEngine;

namespace PlayerCharacter
{
    public class PlayerInput : MonoBehaviour
    {
        
        [SerializeField] private float moveAxisDeadZone = 0.2f;    
        [SerializeField] private float dashDelayTime = 1.5f;           

        public Vector2 MoveInput { get; private set; }      
        //public Vector2 LastMoveInput { get; private set; } 
        public Vector2 CameraInput { get; private set; }    
        public bool JumpInput { get; private set; }         
        public bool DashInput { get; private set; }         
        //public bool HasMoveInput { get; private set; }      


        public void UpdateInput()
        {
            // Return if dashed
            if (DashInput)
                return;

            Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (Mathf.Abs(moveInput.x) < moveAxisDeadZone)
                moveInput.x = 0.0f;
            if (Mathf.Abs(moveInput.y) < moveAxisDeadZone)
                moveInput.y = 0.0f;

            bool hasMoveInput = moveInput.sqrMagnitude > 0.0f;

            //if (HasMoveInput && !hasMoveInput)
            //    LastMoveInput = moveInput;

            MoveInput = moveInput;          // Update MoveInput
            //HasMoveInput = hasMoveInput;    

            CameraInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse y"));

            JumpInput = Input.GetButton("Jump");

            // Can dash if not jumped
            if (!JumpInput)
                DashInput = Input.GetMouseButtonUp(1);
            if (DashInput)
                Invoke("SetDashInput", dashDelayTime);
        }

        // Set DashInput Value
        private void SetDashInput()
        {
            DashInput = false;
        }
    }
}

