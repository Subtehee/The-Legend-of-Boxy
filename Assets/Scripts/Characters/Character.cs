using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Characters
{
    public class Character : MonoBehaviour
    {
        protected Rigidbody _rigidbody = null;

        protected float moveSpeed = 0.0f;


        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

    }

}
