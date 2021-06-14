// ============================
// ���� : 2021-06-14
// �ۼ� : sujeong
// ============================

namespace Characters.Player
{
    public enum MovementState
    {
        IDLE,
        RUN,
        JUMP,
        SPRINT,
        DOWNFALL,
        GLIDING,
        AUTOATTACK,
        STRONGATTACK,
        DIE
    }

    public class PlayerState
    {
        public MovementState currentState;

        public void ChangeState(MovementState state)
        {
            currentState = state;
        }

        public MovementState GetState()
        {
            return currentState;
        }
    }
}