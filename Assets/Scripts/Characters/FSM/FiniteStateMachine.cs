// ============================
// 수정 : 2021-06-29
// 작성 : sujeong
// ============================

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.FSM
{

    public class FiniteStateMachine
    {
        private IState CurrentState = null;
        private IState PreState = null;

        private bool RunnableFixedUpdate = true;    // 애니메이션 전환 딜레이에 맞춰 움직임을 주기 위함
        private bool ExcuteFixedUpdate = true;      // 캐릭터가 움직인 후 상태 체크하기 위함

        // 상태(타입)별 전환 목록 저장
        private Dictionary<Type, List<Transition>> _transitions = new Dictionary<Type, List<Transition>>();
        // 현재 실행중인 상태의 전환 가능 목록
        private List<Transition> _currentTransitions = new List<Transition>();
        // 어떤 상태에서도 전환 가능한 목록 저장
        private List<Transition> _anyTransitions = new List<Transition>();

        private static List<Transition> EmptyTransitions = new List<Transition>(0);

        // Transition Tick
        public void UpdateState()
        {
            // 상태전환 후, FixedUpdate()가 한 번 실행되면 상태 체크 시작
            if (ExcuteFixedUpdate)
            {
                var transition = GetTransition();
                if (transition != null)
                {
                    SetState(transition.To);

                    RunnableFixedUpdate = true;
                    ExcuteFixedUpdate = false;
                }
                CurrentState?.UpdateState();
            }
        }

        public void FixedUpdateState()
        {
            if (RunnableFixedUpdate)
            {
                CurrentState?.FixedUpdateState();
                if (!ExcuteFixedUpdate)
                    ExcuteFixedUpdate = true;
            }
            else
                PreState?.FixedUpdateState();
        }

        public void SetState(IState state)
        {
            if (state == CurrentState)
                return;

            PreState = CurrentState;

            // keep running FixedUpdate
            RunnableFixedUpdate = false;

            CurrentState?.Exit();
            CurrentState = state;

            // 현재 실행중인 상태의 전환 가능 목록 가져오기
            _transitions.TryGetValue(CurrentState.GetType(), out _currentTransitions);
            if (_currentTransitions == null)
                _currentTransitions = EmptyTransitions;

            CurrentState.Enter();
        }

        public void AddTransition(IState from, IState to, Func<bool> predicate)
        {
            // 전환 목록이 있는지 확인
            if (_transitions.TryGetValue(from.GetType(), out var transitions) == false)
            {
                transitions = new List<Transition>();
                _transitions[from.GetType()] = transitions;
            }

            // 전환 가능 상태와 조건 추가
            transitions.Add(new Transition(to, predicate));
        }

        public void AddAnyTransition(IState state, Func<bool> predicate)
        {

            _anyTransitions.Add(new Transition(state, predicate));
        }

        private Transition GetTransition()
        {
            foreach (var transition in _anyTransitions)
                if (transition.Condition())
                    return transition;

            foreach (var transition in _currentTransitions)
            {
                if (transition.Condition())
                    return transition;
            }

            return null;
        }
    }
}
