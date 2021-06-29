// ============================
// 수정 : 2021-06-28
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Landing : ActionBase
    {

        private readonly float _gravity = 0.0f;

        public PlayerAction_Landing(Character owner, States state, float gravity)
            : base(owner, state) 
        {
            _gravity = gravity;
        }

        public override void Enter()
        {
            base.Enter();

            _owner.SetAnimtaionDelay(0.11f);
        }

        public override void FixedUpdateState()
        {
            _owner.OnDecel();
            _owner.OnGravity(_gravity);
        }
    }

}


