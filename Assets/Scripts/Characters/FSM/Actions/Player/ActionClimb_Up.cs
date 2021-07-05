// ============================
// 수정 : 2021-07-05
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    public class ActionClimb_Up : ActionBase
    {
        private readonly float _jumpForce = 0.0f;

        public ActionClimb_Up(Character owner, States state, float jumpForce)
            : base(owner, state)
        {
            _jumpForce = jumpForce;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

        public override void Exit()
        {

        }
    }

}

