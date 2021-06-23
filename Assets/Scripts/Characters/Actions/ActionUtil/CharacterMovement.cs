using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.FSM.States
{
    [CreateAssetMenu(fileName ="Movement", menuName ="Actions/Movement")]
    public class CharacterMovement : ScriptableObject
    {
        public void Move(Rigidbody rigidbody, Vector3 direction, float speed, float accel)
        {
            float curSpeed = rigidbody.velocity.magnitude;
            //float targetSpeed = Mathf.Lerp(curSpeed, speed, Time.fixedDeltaTime);
            float targetSpeed = Mathf.MoveTowards(curSpeed, speed, accel * Time.fixedDeltaTime);

            rigidbody.velocity = direction * targetSpeed;
        }

        public void Bracking(Rigidbody rigidbody, float dclr)
        {
            rigidbody.velocity = Vector3.MoveTowards(rigidbody.velocity,
                new Vector3(0.0f, rigidbody.velocity.y, 0.0f), dclr * Time.fixedDeltaTime);
        }

        public void Fall(Rigidbody rigidbody, float gravity)
        {
            rigidbody.velocity = new Vector3(rigidbody.velocity.x, -gravity, rigidbody.velocity.z);
        }
    }
}
