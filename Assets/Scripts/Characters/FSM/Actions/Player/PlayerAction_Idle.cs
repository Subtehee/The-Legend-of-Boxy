// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Idle : ActionBase
    {
        private readonly float _gravity = 0.0f;

        public PlayerAction_Idle(Character owner, States state)
            : base(owner, state) { }

        public override void Enter()
        {
            _owner.curSpeed = 0.0f;      // init Speed

            base.Enter();
        }

        public override void FixedUpdateState() 
        {
            _owner.OnGravity(_gravity);
            _owner.OnDecel();
        }

    }

}
