// ============================
// 수정 : 2021-06-21
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

        public PlayerAction_Idle(PlayerCharacter player, Animator animator)
        {
            _player = player;
            _animator = animator;
        }

        public void Enter()
        {
            _player.State = Player.States.IDLE;
        }

        public void UpdateState() { }

        public void FixedUpdateState() 
        {

        
        }
        public void Exit() 
        { 
            Debug.Log("Exit from IDLE State"); 
        }
    }

}
