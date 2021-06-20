// ============================
// 수정 : 2021-06-21
// 작성 : sujeong
// ============================

using System;

namespace Characters.FSM
{
    public class Transition
    {
        Enum Id { get; set; }
        public Func<bool> Condition { get; set; }
        public IState To { get; set; }

        public Transition(Enum id, IState to, Func<bool> condition)
        {
            Id = id;
            To = to;
            Condition = condition;
        }

    }

}

