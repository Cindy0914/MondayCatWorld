using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MondayCatWorld.Character
{
    // 펫의 모델을 담당하는 클래스
    public class PetModel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;
        public Animator Animator => animator;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
    }
}