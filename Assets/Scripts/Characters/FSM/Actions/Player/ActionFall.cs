// ============================
// ���� : 2021-06-28
// �ۼ� : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    public class ActionFall : ActionBase
    {

        private readonly float _gravity = 0.0f;

        public ActionFall(Character owner, States state, float gravity)
            :base(owner, state)
        {
            _gravity = gravity;
        }

        public override void Enter()
        {
            // deduplicate animation
            if (_owner.State == States.JUMP)
            {
                _owner.State = _state;
                return;
            }

            base.Enter();
        }

        public override void FixedUpdateState()
        {
            _owner.OnGravity(_gravity);
        }
    }
}
