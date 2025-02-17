using UnityEngine;

namespace MondayCatWorld.Character
{

    public class Player : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer sr;
        [SerializeField] private Transform tr;
        [SerializeField] private Animator anim;

        [SerializeField] private float speed = 5f;

        private readonly int isWalking = Animator.StringToHash("IsMoving");
        private readonly int direction = Animator.StringToHash("Direction");

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            float hor = Input.GetAxisRaw("Horizontal");
            float ver = Input.GetAxisRaw("Vertical");

            if (hor == 0 && ver == 0)
            {
                anim.SetBool(isWalking, false);
            }
            else
            {
                anim.SetBool(isWalking, true);
                tr.position += new Vector3(hor, ver, 0) * (speed * Time.deltaTime);
            }

            SwitchDirection(hor, ver);
        }

        private void SwitchDirection(float hor, float ver)
        {
            var walked = anim.GetBool(isWalking);
            if (!walked) return;

            if (hor > 0)
            {
                sr.flipX = false;
                anim.SetInteger(direction, (int)Direction.Right);
            }
            else if (hor < 0)
            {
                sr.flipX = true;
                anim.SetInteger(direction, (int)Direction.Left);
            }
            else if (ver > 0)
            {
                anim.SetInteger(direction, (int)Direction.Up);
            }
            else if (ver < 0)
            {
                anim.SetInteger(direction, (int)Direction.Down);
            }
        }
    }

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }
}