using UnityEngine;

public class Pet : MonoBehaviour
{
    private static readonly int isMoving = Animator.StringToHash("IsMoving");
    [SerializeField] private Animator animator;
    [SerializeField] private Spri

    private Transform playerTr;
    private float followSpeed;
    private float followDistance;

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        float distance = Vector3.Distance(playerTr.position, transform.position);
        
        if (distance > followDistance)
        {
            animator.SetBool(isMoving, true);
            transform.position = Vector3.MoveTowards(transform.position, playerTr.position, followSpeed * Time.deltaTime);
        }
        else
        {
            animator.SetBool(isMoving, false);
        }
        
        
    }
}
