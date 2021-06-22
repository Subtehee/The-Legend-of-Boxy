// ============================
// 수정 : 2021-06-22
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.States
{
    public class PlayerAction_Move : IState
    {
        private readonly PlayerCharacter _player = null;
        private readonly Rigidbody _rigidbody = null;
        private readonly Animator _animator = null;

        private readonly float _gravity = 0.0f;
        private readonly float _accel = 2.0f;
        private readonly float _runSpeed = 0.0f;
        private readonly float _sprintSpeed = 0.0f;
        private readonly float _rotationSmoothTime = 0.1f;

        private float curSpeed = 0.0f;
        private float moveSpeed = 0.0f;             // run / sprint
        private float smoothVelocity = 0.0f;        // for smoothDamp

        private Vector2 _moveInput = Vector2.zero;  // move direction
        private Transform _direction = null;        // Get camera direction


        // TODO : Add Animatior Param

        public PlayerAction_Move(PlayerCharacter player, Rigidbody rigidbody, Animator animator, 
                                float gravity, float runSpeed, float sprintSpeed, float accel)
        {
            _player = player;
            _rigidbody = rigidbody;
            _animator = animator;
            _gravity = -gravity;
            _runSpeed = runSpeed;
            _sprintSpeed = sprintSpeed;
            //_accel = accel;
        }

        public void Enter() 
        {
            //Debug.Log("Enter Move State");

            _player.State = Player.States.MOVE;
        }

        // moveDirection == MoveInput * Camera.forward.normalized
        public void UpdateState()
        {
            _moveInput = InputManager.Instance.MoveInput;    

            _direction = _player.playerDirection;

            // Change speed
            if (InputManager.Instance.SprintInput)
            {
                moveSpeed = _sprintSpeed;
                Debug.Log("Sprinting...");
                // Stamina Set
            }
            else moveSpeed = _runSpeed;
        }

        public void FixedUpdateState()
        {
            RotatePlayer();
            MovePlayer();
        }

        private void RotatePlayer()
        {
            float targetAngle = Mathf.Atan2(_moveInput.x, _moveInput.y) * Mathf.Rad2Deg 
                                            + _direction.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(_player.transform.eulerAngles.y, targetAngle, 
                                                ref smoothVelocity, _rotationSmoothTime);
            _player.transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
        }

        private void MovePlayer()
        {
            // (currentSpeed --> targetSpeed) < MaxSpeed
            // 급격하게 증가하고 급격하게 감소해야함 --> targetSpeed와 currentSpeed의 차이로 수치변화의 경사를 지정
            // targetSpeed - currentSpeed / 
            _rigidbody.AddForce(GetMoveDirection() * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
            _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, moveSpeed);

            Debug.Log(_rigidbody.velocity);
        }

        private Vector3 GetMoveDirection()
        {
            return (_direction.forward * _moveInput.y + _direction.right * _moveInput.x).normalized;
        }

        public void Exit()
        {
            // TODO : animator set
        }

    }

}
