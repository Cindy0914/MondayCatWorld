using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MondayCatWorld.Character
{
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Animator Animator => animator;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
    }
}