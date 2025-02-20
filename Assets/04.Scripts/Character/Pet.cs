using UnityEngine;

namespace MondayCatWorld.Character
{
    public class Pet : MonoBehaviour
    {
        [SerializeField] private Transform playerTr;
        [SerializeField] private Transform Tr;
        private readonly int isMoving = Animator.StringToHash("IsMoving");
        private readonly int direction = Animator.StringToHash("Direction");
        private const float followDistance = 1f;

        private PetModel model;
        private float followSpeed;
        private bool isSpawned = false;

        private void Update()
        {
            if (!isSpawned) return;
            Move();
        }

        private void Move()
        {
            var distance = Vector3.Distance(playerTr.position, Tr.position);
            var moveDirection = (playerTr.position - Tr.position).normalized;

            Vector3 targetPosition = playerTr.position - (moveDirection * followDistance);
            
            
            if (distance > followDistance * 1.1f)
            {
                model.Animator.SetBool(isMoving, true);
                Tr.position = Vector3.Lerp(Tr.position, targetPosition, followSpeed * Time.deltaTime);
                SwitchDirection(moveDirection);
            }
            else
            {
                model.Animator.SetBool(isMoving, false);
            }
        }

        private void SwitchDirection(Vector3 dir)
        {
            dir.x = Mathf.Round(dir.x);
            dir.y = Mathf.Round(dir.y);
            
            if (dir.x > 0)
            {
                model.Animator.SetInteger(direction, (int)Direction.Right);
                model.SpriteRenderer.flipX = false;
            }
            else if (dir.x < 0)
            {
                model.Animator.SetInteger(direction, (int)Direction.Left);
                model.SpriteRenderer.flipX = true;
            }
            else if (dir.y > 0)
            {
                model.Animator.SetInteger(direction, (int)Direction.Up);
            }
            else if (dir.y < 0)
            {
                model.Animator.SetInteger(direction, (int)Direction.Down);
            }
        }

        public void SetModel(PetData data)
        {
            if (model)
            {
                Destroy(model.gameObject);
            }

            Tr.parent = null;
            model = Instantiate(data.Prefab, transform).GetComponent<PetModel>();
            model.transform.localPosition = Vector3.zero;
            followSpeed = data.FollowSpeed;
            isSpawned = true;
        }
    }
}