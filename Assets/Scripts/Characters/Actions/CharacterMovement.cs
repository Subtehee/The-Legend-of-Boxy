// ============================
// ���� : 2021-06-14
// �ۼ� : sujeong
// ============================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Actions
{

    public abstract class CharacterMovement : ScriptableObject
    {
        // Move to horizontal direction
        public abstract void HorizontalMove(Transform rigid, float moveSpeed, Vector3 direction);
    }
}

