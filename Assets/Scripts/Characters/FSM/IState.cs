// ============================
// 수정 : 2021-06-17
// 작성 : sujeong
// ============================

/*
 
IState       : 상태의 실행과 종료 정의
FSMHandler   : 상태를 변화시키는 함수 정의  --> ScriptableObject (==StateMachine)
CharacterFSM : Character 특성에 따라 IState 재정의

CharacterController
    - CharacterFSM -> owner지정

 */

namespace Characters.State
{
    public interface IState
    {
        public void Enter();    // 상태 입력
        public void Execute();  // 상태 실행
        public void Exit();     // 상태 종료
    }

}

