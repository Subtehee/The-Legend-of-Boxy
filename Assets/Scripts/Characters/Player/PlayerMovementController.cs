using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Characters.Player
{
    [CreateAssetMenu(fileName = "PlayerController", menuName ="PlayerController")]
    public class PlayerMovementController : ScriptableObject
    {
        private PlayerMoveInput _playerMoveInput = null;
        private PlayerAttackInput _playerAttackInput = null;
        private PlayerCamera _playerCamera = null;

        private void Awake()
        {
            _playerMoveInput = FindObjectOfType<PlayerMoveInput>();
            _playerAttackInput = FindObjectOfType<PlayerAttackInput>();
            _playerCamera = FindObjectOfType<PlayerCamera>();
        }



    }

}

