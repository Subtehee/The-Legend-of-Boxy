using UnityEngine;

namespace Characters.Player
{
    [CreateAssetMenu(fileName = "PlayerStatus", menuName = "ScriptableObject/PlayerStatus")]
    public class PlayerStatus : CharacterStatus
    {
        private PlayerInput _playerInput = null;
        private PlayerCamera _playerCamera = null;

        public override void InitStatus()
        {
            _playerInput = FindObjectOfType<PlayerInput>();
            _playerCamera = FindObjectOfType<PlayerCamera>();


        }

        public override void OnCharacterUpdate()
        {

        }

        public override void OnCharacterFixedUpdate()
        {

        }
    }

}
