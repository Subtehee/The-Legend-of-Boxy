using UnityEngine;


namespace PlayerCharacter
{
    [System.Serializable]
    public class MovementSettings
    {
        public float acceleration = 25.0f;      
        public float decceleration = 25.0f;
        public float maxSpeed = 8.0f;
        public float jumpForce = 10.0f;
        public float jumpAbortForce = 10.0f;
    }

    [System.Serializable]
    public class GravitySettings
    {
        public float airborneGravity = 20.0f;
        public float groundedGravity = 5.0f;
        public float jumpGravity = 30.0f;
        public float maxFallSpeed = 40.0f;
    }

    [System.Serializable]
    public class RotationSettings
    {
        // Camera pitch angle min, max
        [Header("Control Rotation")]
        public float minPitchAngle = -45.0f;
        public float maxPitchAngle = 75.0f;

    }

    public class PlayerMoveController : MonoBehaviour
    {
        // Get setting classes
        public MovementSettings MovementSettings;
        public GravitySettings GravitySettings;
        public RotationSettings RotationSettings;

        private PlayerInput _playerInput = null;
        private Animator _animator = null;
        private Rigidbody _rigidbody = null;
        

        //public bool isGrounded


        private void Awake()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        void Start()
        {

        }

        void Update()
        {
            UpdateInputs();
        }

        private void FixedUpdate()
        {
            
        }

        private void UpdateInputs()
        {
            _playerInput.UpdateInput();

            SetInputs();

        }

        private void SetInputs()
        {

        }
    }

}
