// ============================
// 수정 : 2021-06-21
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.States
{
    public class PlayerAction_Run : IState
    {
        private readonly PlayerCharacter _player = null;
        private readonly Rigidbody _rigidbody = null;
        private readonly Animator _animator = null;
        private readonly float _moveSpeed = 0.0f;

        private Vector2 _moveInput = Vector2.zero;
        private Transform _direction = null;

        // TODO : Add Animatior Param

        public PlayerAction_Run(PlayerCharacter player, Rigidbody rigidbody, Animator animator, float moveSpeed)
        {
            _player = player;
            _rigidbody = rigidbody;
            _animator = animator;
            _moveSpeed = moveSpeed;
        }

        public void Enter() 
        {
            _player.State = Player.States.RUN;
            Debug.Log("Enter to Run State"); 
        }

        // moveDirection == MoveInput * Camera.forward.normalized
        public void UpdateState()
        {
            _moveInput = _player.moveDirection;
            _direction = _player.playerDirection;
        }

        public void FixedUpdateState()
        {
            // Rigidbody Rotation

            _rigidbody.AddForce(((_direction.right * _moveInput.x) + (_direction.forward * _moveInput.y)) 
                * _moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }

        public void Exit()
        {
            // TODO : animator set
        }

    }

}
