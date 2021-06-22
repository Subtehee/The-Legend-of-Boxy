// ============================
// 수정 : 2021-06-22
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.States
{
    public class PlayerAction_Idle : IState
    {
        private PlayerCharacter _player = null;
        private Animator _animator = null;

        private float _gravity = 0.0f;

        public PlayerAction_Idle(PlayerCharacter player, Animator animator, float gravity)
        {
            _player = player;
            _animator = animator;
            _gravity = -gravity;
        }

        public void Enter()
        {
            //Debug.Log("Enter Idle State");

            _player.State = Player.States.IDLE;
        }

        public void UpdateState() { }

        public void FixedUpdateState() { }

        public void Exit() { }
    }

}
