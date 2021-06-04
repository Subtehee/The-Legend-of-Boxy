/* 20210604_
 * 
 * 
 */

using UnityEngine;


namespace PlayerCharacter
{
    // ĳ���� ������ �Ķ����
    [System.Serializable]
    public class MovementSettings
    {
        public float acceleration = 25.0f;      // ���ӵ�
        public float decceleration = 25.0f;     // ����
        public float MaxHorizontalSpeed = 8.0f; // �ִ� �̵�
        public float JumpSpeed = 10.0f;         // ���� �ӵ�
        public float JumpAbortSpeed = 10.0f;    // ���� �ߴ� �ӵ�??
                                                // 
    }

    // ĳ���� �߷� �Ķ����
    [System.Serializable]
    public class GravitySettings
    {
        public float Gravity = 20.0f;           // ���߿����� �߷�
        public float GroundedGravity = 5.0f;    // ���� �� �߷�
        public float MaxFallSpeed = 40.0f;      // �ִ� ���� �ӵ�

    }

    // ĳ���Ϳ� ī�޶� ȸ�� �Ķ����
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

        // ĳ������ ȸ�� �ӵ� min, max
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

        private float _targetHorizontalSpeed;   // ��ǥ �ӵ�
        private float _horizontalSpeed;         // ���������� �ӵ�
        private float _verticalSpeed;           // ���߿����� �ӵ�

        private Rigidbody rigid = null;
        private Vector2 _controlRoation = Vector2.zero;     // ȸ�� ����
        private Vector3 _movementInput = Vector3.zero;      // �Էµ� ���Ⱚ
        private Vector3 _lastMovementInput = Vector3.zero;  // ���������� �Էµ� ���Ⱚ
        private bool _hasMovementInput = false;             // �Է°��� ���Դ��� ����
        private bool _jumpInput = false;                    // ���� Ű�Է��� ���Դ��� ����

        public bool isGrounded { get; private set; }

        private void Awake()
        {
            
        }



    }

}
