// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class PlayerAction_Idle : ActionBase
    {
        private readonly float _gravity = 0.0f;
        private readonly float _decel = 0.0f;

        public PlayerAction_Idle(Character owner, States state, float gravity, float decel)
            : base(owner, state) 
        {
            _gravity = gravity;
            _decel = decel;
        }

        public override void Enter()
        {
            _owner.curSpeed = 0.0f;      // init Speed

            base.Enter();
            //Debug.Log("Enter the Idle State");
        }

        public override void FixedUpdateState() 
        {
            _owner.OnGravity(_gravity);
            _owner.OnDecel();
        }

    }

}
