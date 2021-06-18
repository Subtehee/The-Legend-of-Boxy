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

namespace Characters.State
{
    public interface IState
    {
        public void Enter();    // ���� �Է�
        public void Execute();  // ���� ����
        public void Exit();     // ���� ����
    }

}

