// ============================
// ���� : 2021-06-23
// �ۼ� : sujeong
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
        private readonly float _runSpeed = 0.0f;
        private readonly float _sprintSpeed = 0.0f;
        private readonly float _rotationSmoothTime = 0.01f;

        private float curSpeed = 0.0f;          // current Speed
        private float moveSpeed = 0.0f;         // run / sprint
        private float smoothVelocity = 0.0f;    // for smoothDamp

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
            _player.State = Player.States.RUN;
            _animator.SetInteger("State", (int)_player.State);

            curSpeed = 0.0f;
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
            // �ް��ϰ� �����ϰ� �ް��ϰ� �����ؾ��� --> targetSpeed�� currentSpeed�� ���̷� ��ġ��ȭ�� ��縦 ����

            float targetSpeed = Mathf.Lerp(curSpeed, moveSpeed, Time.fixedDeltaTime);
            _rigidbody.velocity = GetMoveDirection() * moveSpeed + _player.transform.up * _gravity;
            // TODO : �ӵ� ���ƽ�ϰ� �ø���

            //_rigidbody.AddForce(GetMoveDirection() * moveSpeed, ForceMode.VelocityChange);
            //_rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, moveSpeed);
        }

        private Vector3 GetMoveDirection()
        {
            return (_direction.forward * _moveInput.y + _direction.right * _moveInput.x).normalized;
        }

        public void Exit() { }

    }

}