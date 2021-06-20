// ============================
// ���� : 2021-06-17
// �ۼ� : sujeong
// ============================

/*
 
IState       : ������ ����� ���� ����
FSMHandler   : ���¸� ��ȭ��Ű�� �Լ� ����  --> ScriptableObject (==StateMachine)
CharacterFSM : Character Ư���� ���� IState ������

CharacterController
    - CharacterFSM -> owner����
 */

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

