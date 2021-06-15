// ============================
// 수정 : 2021-06-15
// 작성 : sujeong
// ============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Characters
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody))]
    public class Character : MonoBehaviour
    {
        public Controller Controller = null;

        protected Rigidbody rigid = null;
        protected Animator anim = null;
        protected Vector3 _moveDirection = Vector3.zero;

        protected virtual void Awake()
        {
            if (Controller == null)
                Controller = GetComponent<Controller>();
        }

        protected virtual void Update()
        {
            Controller.UpdateControl();
        }

        protected virtual void FixedUpdate()
        {
            Controller.FixedUpdateControl();
            _moveDirection = Controller.moveDirection;
        }
    }
}
