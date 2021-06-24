// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Dash : ActionBase
    {

        private readonly float _accel = 0.0f;

        public PlayerAction_Dash(PlayerCharacter player, States state, float accel)
        {
            this.state = state;
            _owner = player;
            _accel = accel;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateState() 
        {

        }
        
        public override void FixedUpdateState() { }
        public override void Exit() { }
    }

}


