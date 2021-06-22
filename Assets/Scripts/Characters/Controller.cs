// ============================
// 수정 : 2021-06-22
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters
{
    public class Controller : MonoBehaviour
    {
        public LayerMask staticLayer = 0;

        protected bool IsGrounded = false;    // Check Grounded

        protected virtual void Awake()
        {
            if (staticLayer == 0)
                staticLayer = LayerMask.NameToLayer("STATICMESH");
        }

        public virtual void UpdateControl() { }         // Update()

        public virtual void FixedUpdateControl() { }    // FixedUpdate()

        protected virtual void SetCharacterState() { }  // Set FSM
    }
}

