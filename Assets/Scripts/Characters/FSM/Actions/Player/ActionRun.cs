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
        private readonly Transform _transform = null;
        private readonly float _runSpeed = 0.0f;
        private readonly float _gravity = 0.0f;
        private readonly float _rotSpeed = 0.0f;
        private readonly float _accel = 0.0f;

        private float m_curSpeed = 0.0f;
        private States m_currentState = States.RUN;

        public ActionRun(Character owner, float runSpeed, float gravity, float rotSpeed, float accel)
        {
            _owner = owner;
            _transform = owner.transform;
            _runSpeed = runSpeed;
            _gravity = gravity;
            _rotSpeed = rotSpeed;
            _accel = accel;
        }

        public override void Enter()
        {
            _owner.State = m_currentState;
            _owner.ToAnimaition(m_currentState.GetHashCode());

            m_curSpeed = _owner.curSpeed;
        }

        public override void UpdateState()
        {
            _owner.UpdateMoveDirection();

        }

        public override void FixedUpdateState()
        {
            _owner.OnRotate(_rotSpeed);

            float targetSpeed = Mathf.Lerp(m_curSpeed, _runSpeed, _accel * Time.deltaTime);
            Vector3 _moveDirection = _owner.moveDirection.normalized;

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
