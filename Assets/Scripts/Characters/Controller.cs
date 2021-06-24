// ============================
// 수정 : 2021-06-24
// 작성 : sujeong
// ============================

using UnityEngine;

namespace Characters
{
    public class Controller : MonoBehaviour
    {
        public LayerMask StaticLayer = 0;

        protected bool IsGrounded = false;    // Check Grounded

        protected virtual void Awake()
        {
            if (StaticLayer == 0)
                StaticLayer = LayerMask.NameToLayer("STATICMESH");
        }

        public virtual void UpdateControl() { }         // Update()
        public virtual void FixedUpdateControl() { }    // FixedUpdate()


        //
    }
}

