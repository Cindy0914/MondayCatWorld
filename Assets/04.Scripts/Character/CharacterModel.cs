using UnityEngine;

namespace MondayCatWorld.Character
{
    // 캐릭터의 모델을 담당하는 클래스
    public class CharacterModel : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Animator Animator => animator;
        public SpriteRenderer SpriteRenderer => spriteRenderer;
    }
}