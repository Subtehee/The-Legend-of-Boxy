// @ Created : 2021-06-10
//
//

using UnityEngine;

namespace Characters.Player
{

    [CreateAssetMenu(fileName ="PlayerStat", menuName ="CharacterStat/PlayerStat")]
    public class PlayerStat : CharacterStatus
    {
        public float glidingGravity = 15.0f;
    }

}
