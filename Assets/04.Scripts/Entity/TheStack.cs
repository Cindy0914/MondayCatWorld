using MondayCatWorld.Managers;
using UnityEngine;

namespace MondayCatWorld.Games
{

    public class TheStack : MonoBehaviour
    {
        // Const Value
        private const string CubeKey = "Cube";
        private const float BoundSize = 3.5f;
        private const float MovingBoundsSize = 3f;
        private const float StackMovingSpeed = 5.0f;
        private const float BlockMovingSpeed = 3.5f;
        private const float ErrorMargin = 0.1f;

        private Vector3 prevBlockPosition;
        private Vector3 desiredPosition;
        private Vector3 stackBounds = new Vector2(BoundSize, BoundSize);

        private Transform lastBlock = null;
        private float blockTransition = 0f;
        private float secondaryPosition = 0f;

        private int stackCount = -1;
        private int comboCount = 0;

        public Color prevColor;
        public Color nextColor;
        
        private bool isMovingX = true;
        private bool isGameOver = true;

        private void Start()
        {
            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            prevBlockPosition = Vector3.down;
            SpawnBlock();
        }
        
        private void Update()
        {
            if (isGameOver) return;
            
            if (Input.GetMouseButtonDown(0))
                SpawnBlock();
            
            MoveBlock();
            transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
        }

        public void StartGame()
        {
            isGameOver = false;
        }

        private bool SpawnBlock()
        {
            // 이전블럭 저장
            if (lastBlock != null)
                prevBlockPosition = lastBlock.localPosition;

            if (PoolManager.Instance.IsExistPool(CubeKey))
            {
                Debug.LogError($"{CubeKey} :: Pool is not exist!");
            }
            
            GameObject newBlock = PoolManager.Instance.Spawn(CubeKey);
            Cube newCube = newBlock.GetComponent<Cube>();
            Transform newTrans = newCube.CubeTr;
            ColorChange(newCube);

            newTrans.parent = transform;
            newTrans.localPosition = prevBlockPosition + Vector3.up;
            newTrans.localRotation = Quaternion.identity;
            newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

            stackCount++;

            desiredPosition = Vector3.down * stackCount;
            blockTransition = 0f;

            lastBlock = newTrans;

            return true;
        }

        private void MoveBlock()
        {
            blockTransition += Time.deltaTime * BlockMovingSpeed;
            float movePosition = Mathf.PingPong(blockTransition, BoundSize) - BoundSize / 2;

            // X축 이동
            if (isMovingX)
            {
                lastBlock.localPosition = new Vector3(movePosition * MovingBoundsSize, stackCount, secondaryPosition);
            }
            else
            {
                lastBlock.localPosition = new Vector3(secondaryPosition, stackCount, -movePosition * MovingBoundsSize);
            }
        }

        private Color GetRandomColor()
        {
            float r = Random.Range(100f, 250f) / 255f;
            float g = Random.Range(100f, 250f) / 255f;
            float b = Random.Range(100f, 250f) / 255f;

            return new Color(r, g, b);
        }

        private void ColorChange(Cube cube)
        {
            Color applyColor = Color.Lerp(prevColor, nextColor, (stackCount % 11) / 10f);

            if (cube.CubeRenderer == null)
            {
                Debug.Log("Renderer is NULL!");
                return;
            }

            cube.CubeRenderer.material.color = applyColor;
            var mainCam = TheStackSceneBase.Instance.MainCamera;
            mainCam.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);

            if (applyColor.Equals(nextColor) == true)
            {
                prevColor = nextColor;
                nextColor = GetRandomColor();
            }
        }
    }
}