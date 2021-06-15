// ============================
// ���� : 2021-06-14
// �ۼ� : sujeong
// ============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters
{
    public class Controller : MonoBehaviour
    {
        [HideInInspector] public Vector3 moveDirection = Vector3.zero;

        public LayerMask staticLayer = 0;
        protected bool IsGrounded = false;    // Check Grounded

        protected virtual void Awake()
        {
            if (staticLayer == 0)
                staticLayer = LayerMask.NameToLayer("STATICMESH");
        }

        public virtual void UpdateControl() { }         // Update()

        public virtual void FixedUpdateControl()        // FixedUpdate()
        {
            CheckGrounded();
        }

        protected virtual void SetCharacterState() { }  // Set FSM
        
        // ĳ���Ͱ� ���� �������� üũ
        protected void CheckGrounded()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 1.0f, staticLayer))
            {
                if (hit.distance < float.Epsilon)
                    IsGrounded = true;
            }
            else IsGrounded = false;
        }
    }
}

