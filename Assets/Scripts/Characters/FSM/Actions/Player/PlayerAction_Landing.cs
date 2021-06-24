// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Landing : ActionBase
    {
        public PlayerAction_Landing(Character owner, States state)
        {
            this.state = state;
            _owner = owner;
        }

        public override void Enter()
        {
            base.Enter();
        }
    }

}


