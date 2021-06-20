// ============================
// 수정 : 2021-06-21
// 작성 : sujeong
// ============================


namespace Characters.FSM
{
    public interface IState
    {
        public void Enter();
        public void UpdateState();     
        public void FixedUpdateState();
        public void Exit();     
    }

}

