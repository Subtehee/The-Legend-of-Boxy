using UnityEngine;

namespace Characters
{
    public class MoveSpeed
    {

    }

    public class Gravity
    {

    }


    public abstract class CharacterStatus : ScriptableObject
    {
        public abstract void InitStatus();
        public abstract void OnCharacterUpdate();
        public abstract void OnCharacterFixedUpdate();

    }

}
