// ============================
// 수정 : 2021-07-01
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Run : ActionBase
    {

        private readonly float _runSpeed = 0.0f;
        private readonly float _sprintSpeed = 0.0f;
        private readonly float _gravity = 0.0f;
        private readonly float _rotSpeed = 0.0f;
        private readonly float _accel = 0.0f;

        private bool m_toggle = false;
        private float m_moveSpeed = 0.0f;
        private States m_currentState = 0.0f;

        public PlayerAction_Run(Character owner, States state, float runSpeed, 
                                float sprintSpeed, float gravity, float rotSpeed, float accel)
            : base(owner, state) 
        {
            _runSpeed = runSpeed;
            _sprintSpeed = sprintSpeed;
            _gravity = gravity;
            _rotSpeed = rotSpeed;
            _accel = accel;
        }

        public override void Enter()
        {
            base.Enter();
            m_moveSpeed = _runSpeed;
        }

        public override void UpdateState()
        {
            _owner.UpdateMoveDirection();

            // Change speed and animation to Run or Sprint
            if (!m_toggle && InputManager.Instance.SprintInput)
            {
                m_moveSpeed = _sprintSpeed;
                ChangeAnim(_state + 1);

                m_toggle = true;
            }
            else if(m_toggle && !InputManager.Instance.SprintInput)
            {
                m_moveSpeed = _runSpeed;
                ChangeAnim(_state);

                m_toggle = false;
            }
        }

        public override void FixedUpdateState()
        {
            _owner.OnRotate(_rotSpeed);
            _owner.OnMove(m_moveSpeed, _accel);
            _owner.OnGravity(_gravity);
        }

        private void ChangeAnim(States changeState)
        {
            _owner.ToAnimaition(changeState.GetHashCode());
        }
    }

}
