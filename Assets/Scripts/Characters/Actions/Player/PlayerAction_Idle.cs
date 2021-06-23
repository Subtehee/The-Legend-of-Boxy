// ============================
// 수정 : 2021-06-23
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.States
{
    public class PlayerAction_Idle : IState
    {

        private readonly PlayerCharacter _player = null;
        private readonly Animator _animator = null;
        private readonly Rigidbody _rigidbody = null;
        private readonly float _gravity = 0.0f;
        private readonly float _accel = 0.0f;

        public PlayerAction_Idle(PlayerCharacter player, Rigidbody rigidbody, Animator animator, float gravity, float accel)
        {
            _player = player;
            _rigidbody = rigidbody;
            _animator = animator;
            _gravity = -gravity;
            _accel = accel;
        }

        public void Enter()
        {
            //Debug.Log("Enter Idle State");

            _player.State = Player.States.IDLE;
            _animator.SetInteger("State", (int)_player.State);
        }

        public void UpdateState() { }

        public void FixedUpdateState() 
        {
            _rigidbody.velocity = Vector3.MoveTowards(_rigidbody.velocity,
                new Vector3(0.0f, _rigidbody.velocity.y, 0.0f), _accel * Time.fixedDeltaTime);
        }

        public void Exit() { }
    }

}
