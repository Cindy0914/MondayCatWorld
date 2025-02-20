using UnityEngine;

namespace MondayCatWorld.Character
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private Transform tr;
        [SerializeField] private Pet pet;
        
        private CharacterModel model = null;
        private readonly int isWalking = Animator.StringToHash("IsMoving");
        private readonly int direction = Animator.StringToHash("Direction");
        private const float speed = 5f;
        
        public Transform Tr => tr;
        public Pet Pet => pet;
        
        private void Update()
        {
            Move();
        }

        private void Move()
        {
            if (!model)
                return;
            
            float hor = Input.GetAxisRaw("Horizontal");
            float ver = Input.GetAxisRaw("Vertical");

            if (hor == 0 && ver == 0)
            {
                model.Animator.SetBool(isWalking, false);
            }
            else
            {
                model.Animator.SetBool(isWalking, true);
                tr.position += new Vector3(hor, ver, 0) * (speed * Time.deltaTime);
            }

            SwitchDirection(hor, ver);
        }

        private void SwitchDirection(float hor, float ver)
        {
            var walked = model.Animator.GetBool(isWalking);
            if (!walked) return;

            if (hor > 0)
            {
                model.SpriteRenderer.flipX = false;
                model.Animator.SetInteger(direction, (int)Direction.Right);
            }
            else if (hor < 0)
            {
                model.SpriteRenderer.flipX = true;
                model.Animator.SetInteger(direction, (int)Direction.Left);
            }
            else if (ver > 0)
            {
                model.Animator.SetInteger(direction, (int)Direction.Up);
            }
            else if (ver < 0)
            {
                model.Animator.SetInteger(direction, (int)Direction.Down);
            }
        }

        public void SetModel(GameObject prefab)
        {
            if (model != null)
            {
                Destroy(model.gameObject);
            }
            
            var modelObj = Instantiate(prefab, tr);
            modelObj.transform.SetParent(tr);
            modelObj.transform.localPosition = Vector3.zero;
            model = modelObj.GetComponent<CharacterModel>();
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