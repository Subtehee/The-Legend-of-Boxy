// ============================
// 수정 : 2021-06-11
// 작성 : sujeong
// ============================

using UnityEngine;


namespace Characters.Player
{
    public class PlayerCharacter : MonoBehaviour
    {

        public PlayerController PlayerController = null;

        private Rigidbody rigid = null;

        private void Awake()
        {
            PlayerController = GetComponent<PlayerController>();
            rigid = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            PlayerController.UpdateControl();
        }

        private void FixedUpdate()
        {
            PlayerController.FixedUpdateControl();
        }

    }

}
