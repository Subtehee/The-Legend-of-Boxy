using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Characters
{

    public class Character : MonoBehaviour
    {
        [Header("Movement Speed")]
        public float moveSpeed = 0.0f;
        public float acceleration = 0.0f;
        public float sprintSpeed = 0.0f;

        [Header("Gravity")]

        protected Rigidbody m_Rigidbody = null;
        protected float currentSpeed = 0.0f;


        protected bool isGrounded = true;

        private void Awake()
        {
            m_Rigidbody = GetComponent<Rigidbody>();
        }

    }

}
