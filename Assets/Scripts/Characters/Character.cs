// ============================
// 수정 : 2021-06-18
// 작성 : sujeong
// ============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.FSM;

namespace Characters
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class Character : MonoBehaviour
    {
        [SerializeField] private Controller Controller = null;

        protected Rigidbody rigid = null;
        protected Animator anim = null;
        protected Vector3 _moveDirection = Vector3.zero;

        protected FiniteStateMachine FSM = null;

        protected virtual void Awake()
        {
            Controller ??= GetComponent<Controller>();
            FSM = new FiniteStateMachine();
        }

        protected virtual void Update()
        {
            Controller.UpdateControl();

            FSM.UpdateState();
        }

        protected virtual void FixedUpdate()
        {
            Controller.FixedUpdateControl();
            _moveDirection = Controller.moveDirection;

            FSM.FixedUpdateState();
        }
    }
}
