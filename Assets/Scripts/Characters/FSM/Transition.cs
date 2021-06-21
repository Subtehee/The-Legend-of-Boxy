// ============================
// 수정 : 2021-06-21
// 작성 : sujeong
// ============================

using System;

namespace Characters.FSM
{
    public class Transition
    {
        public Func<bool> Condition { get; set; }
        public IState To { get; set; }

        public Transition(IState to, Func<bool> condition)
        {
            To = to;
            Condition = condition;
        }

    }

}

