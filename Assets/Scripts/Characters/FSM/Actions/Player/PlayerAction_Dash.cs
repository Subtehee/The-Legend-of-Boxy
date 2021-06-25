// ============================
// 수정 : 2021-06-25
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Dash : ActionBase
    {

        private readonly float _accel = 0.0f;

        public PlayerAction_Dash(Character owner, States state, float accel)
            : base(owner, state) 
        {
            _accel = accel;
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
            _owner.AddImpulseForce(_owner.moveDirection, _accel);

        }

        public override void Exit() { }
    }

}


