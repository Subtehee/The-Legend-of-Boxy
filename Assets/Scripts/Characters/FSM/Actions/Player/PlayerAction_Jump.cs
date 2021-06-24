// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Jump : ActionBase
    {
        private readonly float _jumpForce;
        private readonly float _gravity;

        public PlayerAction_Jump(Character owner, States state, float jumpForce, float gravity)
        {
            this.state = state;
            _owner = owner;
            _jumpForce = jumpForce;
            _gravity = gravity;
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void FixedUpdateState()
        {
            _owner.AddImpulseForce(_owner.transform.up, _jumpForce);
            _owner.OnGravity();
        }

    }

}

