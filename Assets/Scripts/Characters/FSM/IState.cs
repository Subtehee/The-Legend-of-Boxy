// ============================
// ���� : 2021-06-21
// �ۼ� : sujeong
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

