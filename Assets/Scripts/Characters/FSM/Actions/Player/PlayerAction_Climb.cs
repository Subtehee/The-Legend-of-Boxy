// ============================
// 수정 : 2021-07-05
// 작성 : sujeong
// ============================

namespace Characters.FSM.Actions
{
    public class PlayerAction_Climb : ActionBase
    {

        private readonly float _climbSpeed = 0.0f;
        private readonly float _hangForce = 0.0f;

        public PlayerAction_Climb(Character owner, States state)
            : base(owner, state)
        {

        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateState()
        {

        }

        public override void FixedUpdateState()
        {

        }

        public override void Exit()
        {

        }

    }

}


