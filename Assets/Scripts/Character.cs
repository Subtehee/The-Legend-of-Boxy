/* 20210604_
 * 
 * 
 */

using UnityEngine;


namespace PlayerCharacter
{
    // 캐릭터 움직임 파라미터
    [System.Serializable]
    public class MovementSettings
    {
        public float acceleration = 25.0f;      // 가속도
        public float decceleration = 25.0f;     // 감속
        public float MaxHorizontalSpeed = 8.0f; // 최대 이동
        public float JumpSpeed = 10.0f;         // 점프 속도
        public float JumpAbortSpeed = 10.0f;    // 점프 중단 속도??
                                                // 
    }

    // 캐릭터 중력 파라미터
    [System.Serializable]
    public class GravitySettings
    {
        public float Gravity = 20.0f;           // 공중에서의 중력
        public float GroundedGravity = 5.0f;    // 접지 시 중력
        public float MaxFallSpeed = 40.0f;      // 최대 낙하 속도

    }

    // 캐릭터와 카메라 회전 파라미터
    [System.Serializable]
    public class RotationSettings
    {
        // ?
        [Header("Control Rotation")]
        public float MinPithAngle = -45.0f;
        public float MaxPithAngle = 75.0f;

        // ?
        [Header("Character Orientation")]
        [SerializeField] private bool _useControlRotation = false;
        [SerializeField] private bool _orientRotationToMovement = true;

        // 캐릭터의 회전 속도 min, max
        public float MinRotationSpeed = 600.0f;
        public float MaxRotationSpeed = 1200.0f;

        public bool UseControlRotation
        {
            get { return _useControlRotation; }
            set { }
        }
        public bool OrientRotationToMovement
        {
            get { return _orientRotationToMovement; }
            set { }
        }

        private void SetUseControlRotation(bool useControlRotation)
        {
            _useControlRotation = useControlRotation;
            _orientRotationToMovement = !_useControlRotation;
        }

        private void SetOrientRotationToMovement(bool orientRotationToMovement)
        {
            _orientRotationToMovement = orientRotationToMovement;
            _useControlRotation = !_orientRotationToMovement;
        }

    }


    public class Character : MonoBehaviour
    {
        public Controller _Controller = null;      // Controls the Character]

        public MovementSettings movementSettings;
        public GravitySettings gravitySettings;
        public RotationSettings rotationSettings;

        private CharacterController _characterController = null;
        //private CharacterAnimator _characterAnimator;

        private float _targetHorizontalSpeed;   // 목표 속도
        private float _horizontalSpeed;         // 평지에서의 속도
        private float _verticalSpeed;           // 공중에서의 속도

        private Rigidbody rigid = null;
        private Vector2 _controlRoation = Vector2.zero;     // 회전 방향
        private Vector3 _movementInput = Vector3.zero;      // 입력된 방향값
        private Vector3 _lastMovementInput = Vector3.zero;  // 마지막으로 입력된 방향값
        private bool _hasMovementInput = false;             // 입력값이 들어왔는지 여부
        private bool _jumpInput = false;                    // 점프 키입력이 들어왔는지 여부

        public bool isGrounded { get; private set; }

        private void Awake()
        {
            
        }



    }

}
