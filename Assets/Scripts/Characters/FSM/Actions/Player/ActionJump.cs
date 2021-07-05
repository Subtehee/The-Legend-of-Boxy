// ============================
// 수정 : 2021-06-25
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    public class ActionJump : ActionBase
    {
        private readonly float _jumpForce;
        private readonly float _gravity;

        private bool IsJumped = false;

        public ActionJump(Character owner, States state, float jumpForce, float gravity)
            : base(owner, state)
        {
            _jumpForce = jumpForce;
            _gravity = gravity;
        }

        public override void Enter()
        {
            base.Enter();

            IsJumped = false;
        }

        public override void FixedUpdateState()
        {
            if (!IsJumped)
            {
                _owner.AddImpulseForce(_owner.transform.up, _jumpForce);
                IsJumped = true;
            }
            _owner.OnGravity(_gravity);

        }

    }

}

