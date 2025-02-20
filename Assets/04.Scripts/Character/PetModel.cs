using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetModel : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public Animator Animator => animator;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
}