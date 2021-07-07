// ============================
// 수정 : 2021-07-01
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class ActionRun : ActionBase
    {
        private readonly Character _owner = null;
        private readonly Rigidbody _rigidbody = null;
        private readonly float _runSpeed = 0.0f;
        private readonly float _gravity = 0.0f;
        private readonly float _rotSpeed = 0.0f;
        private readonly float _accel = 0.0f;

        private States state = States.RUN;

        public ActionRun(Character owner, Rigidbody rigidbody, float runSpeed, float gravity, float rotSpeed, float accel)
        {
            _owner = owner;
            _rigidbody = rigidbody;
            _runSpeed = runSpeed;
            _gravity = gravity;
            _rotSpeed = rotSpeed;
            _accel = accel;
        }

        public override void Enter()
        {
            _owner.State = state;
            _owner.ToAnimaition(state.GetHashCode());

        }

        public override void UpdateState()
        {
            _owner.UpdateMoveDirection();
        }

        public override void FixedUpdateState()
        {
            Behaviors.OnRotate(_owner.transform, )
            Behaviors.OnMove(_owner, _owner.rigid, _runSpeed, _accel, _owner.moveDirection);

            _owner.OnGravity(_gravity);

        }

        private void ChangeAnim(States changeState)
        {
            _owner.ToAnimaition(changeState.GetHashCode());
        }

        public override void Exit()
        {
            _owner.curSpeed = m_curSpeed;
        }
    }

}
