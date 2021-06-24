// ============================
// 수정 : 2021-06-23
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Run : ActionBase
    {
        public PlayerAction_Run(Character owner, States state)
            : base(owner, state) { }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateState()
        {
            _owner.UpdateMoveDirection();
        }

        public override void FixedUpdateState()
        {
            _owner.OnRotate();
            _owner.OnMove(_owner.Stat.);
            _owner.OnGravity(_owner.);
        }

    }

}
