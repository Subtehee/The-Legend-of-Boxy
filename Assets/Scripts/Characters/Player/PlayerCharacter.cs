using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerCharacter : Character
    {
        public PlayerController _playerController = null;

        protected override void Awake()
        {
            base.Awake();
        }

        protected override void Start()
        {
            base.Start();

        }


        void Update()
        {

        }
    }

}
