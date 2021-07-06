// ============================
// 수정 : 2021-07-05
// 작성 : sujeong
// ============================

using UnityEngine;
using Characters.Player;

namespace Characters.FSM.Actions
{
    public class ActionBase : MonoBehaviour, IState
    {
        protected Behaviors Behaviors = null;

        protected void Awake()
        {
            Behaviors ??= FindObjectOfType<Behaviors>();
        }

        public virtual void Enter() { }
        public virtual void UpdateState() { }
        public virtual void FixedUpdateState() { }
        public virtual void Exit() { }
    }

}

