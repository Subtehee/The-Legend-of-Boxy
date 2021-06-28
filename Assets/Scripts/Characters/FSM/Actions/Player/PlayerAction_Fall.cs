// ============================
// 수정 : 2021-06-28
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Fall : ActionBase
    {

        private readonly float _gravity = 0.0f;

        public PlayerAction_Fall(Character owner, States state, float gravity)
            :base(owner, state)
        {
            _gravity = gravity;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void FixedUpdateState()
        {
            _owner.OnGravity(_gravity);
        }

    }

}
