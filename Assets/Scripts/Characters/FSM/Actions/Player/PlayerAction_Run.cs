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

        private readonly float _runSpeed = 0.0f;
        private readonly float _gravity = 0.0f;
        private readonly float _rotSpeed = 0.0f;

        public PlayerAction_Run(Character owner, States state, float runSpeed, float gravity, float rotSpeed)
            : base(owner, state) 
        {
            _runSpeed = runSpeed;
            _gravity = gravity;
            _rotSpeed = rotSpeed;
        }

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
            _owner.OnRotate(_rotSpeed);
            _owner.OnMove(_runSpeed);
            _owner.OnGravity(_gravity);
        }

    }

}
