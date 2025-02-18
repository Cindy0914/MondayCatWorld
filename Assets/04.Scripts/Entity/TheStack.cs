using MondayCatWorld.Managers;
using UnityEngine;

namespace MondayCatWorld.Games
{
    public class TheStack : MonoBehaviour
    {
        // Const Value
        private const string CubeKey = "Cube";
        private const string BestScoreKey = "BestScore";
        private const string BestComboKey = "BestCombo";
        private const float BoundSize = 3.5f;
        private const float MovingBoundsSize = 3f;
        private const float StackMovingSpeed = 5.0f;
        private const float BlockMovingSpeed = 3.5f;
        private const float ErrorMargin = 0.1f;

        private Vector3 prevBlockPosition;
        private Vector3 desiredPosition;                                    // The Stack이 이동 할 위치(일정한 카메라 시점을 유지하기 위해 사용)
        private Vector3 stackBounds = new Vector2(BoundSize, BoundSize); // 큐브 크기

        private Transform lastBlock = null;
        private float blockTransition = 0f;   // 블럭 이동 시간
        private float secondaryPosition = 0f; // 블럭 이동 위치

        private int stackCount = -1;
        public int Score => stackCount + 10;

        public Color prevColor;
        public Color nextColor;
        private Color lastColor;

        private bool isMovingX = true;
        private bool isGameOver = true;

        public int Combo { get; private set; } = 0;
        public int MaxCombo { get; private set; } = 0;
        public int BestScore { get; private set; } = 0;
        public int BestCombo { get; private set; } = 0;

        private void Start()
        {
            BestScore = PlayerPrefs.GetInt(BestScoreKey, 0);
            BestCombo = PlayerPrefs.GetInt(BestComboKey, 0);
            StackUIPresenter.Instance.InitBestScore(BestScore, BestCombo);

            prevColor = GetRandomColor();
            nextColor = GetRandomColor();

            // 초기 블럭 생성
            prevBlockPosition = Vector3.down;
            SpawnBlock(); // 블럭은 prevBlockPosition의 y+1 위치에 생성되기 때문에 초기값을 -1로 설정
        }

        private void Update()
        {
            if (isGameOver) return;

            if (Input.GetMouseButtonDown(0))
            {
                if (PlaceBlock())
                {
                    SpawnBlock();
                }
                else
                {
                    GameOver();
                }
            }

            MoveBlock();
            transform.position = Vector3.Lerp(transform.position, desiredPosition, StackMovingSpeed * Time.deltaTime);
        }

        public void StartGame()
        {
            isGameOver = false;
        }

        private void SpawnBlock()
        {
            // 이전블럭이 있다면 이전블럭의 위치를 저장
            // 없다면 초기값 그대로 사용
            if (lastBlock != null)
                prevBlockPosition = lastBlock.localPosition;

            if (!PoolManager.Instance.IsExistPool(CubeKey))
            {
                Debug.LogError($"{CubeKey} :: Pool is not exist!");
            }

            GameObject newBlock = PoolManager.Instance.Spawn(CubeKey);
            Cube newCube = newBlock.GetComponent<Cube>();
            newCube.CubeRigidbody.isKinematic = true;
            Transform newTrans = newCube.CubeTr;
            ColorChange(newCube);

            newTrans.parent = transform;
            newTrans.localPosition = prevBlockPosition + Vector3.up;
            newTrans.localRotation = Quaternion.identity;
            newTrans.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

            stackCount++;
            StackUIPresenter.Instance.UpdateScore(Score);

            // lastBlock을 카메라에 비추기 위해 해당 오브젝트가 이동 할 위치를 계산
            desiredPosition = Vector3.down * stackCount;
            blockTransition = 0f;

            // 마지막으로 스폰 된 블럭을 lastBlock으로 설정
            lastBlock = newTrans;
            isMovingX = !isMovingX;
        }

        private void MoveBlock()
        {
            blockTransition += Time.deltaTime * BlockMovingSpeed;

            // 0 ~ boundSize ~ 0 사이의 값
            // - boundSize / 2 ~ boundSize / 2 사이의 값을 가지게 됨 -> -1.75 ~ 1.75
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

        private bool PlaceBlock()
        {
            Vector3 lastPosition = lastBlock.transform.localPosition;

            if (isMovingX)
            {
                // prevBlockPosition.x와 lastPosition.x 사이의 거리
                float deltaX = prevBlockPosition.x - lastPosition.x;
                bool isNegativeNum = deltaX < 0 ? true : false;

                deltaX = Mathf.Abs(deltaX);
                if (deltaX > ErrorMargin) // 오차범위를 넘어서면
                {
                    stackBounds.x -= deltaX;
                    if (stackBounds.x <= 0)
                        return false;

                    float middle = (prevBlockPosition.x + lastPosition.x) / 2;
                    lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                    Vector3 tempPosition = lastBlock.localPosition;
                    tempPosition.x = middle;
                    lastBlock.localPosition = lastPosition = tempPosition;

                    float rubbleHalfScale = deltaX / 2;
                    CreateRubble(new
                                     Vector3(isNegativeNum ? lastPosition.x + stackBounds.x / 2 + rubbleHalfScale : lastPosition.x - stackBounds.x / 2 - rubbleHalfScale,
                                             lastPosition.y, lastPosition.z),
                                 new Vector3(deltaX, 1, stackBounds.y));

                    Combo = 0;
                }
                else
                {
                    CheckCombo();
                    lastBlock.localPosition = prevBlockPosition + Vector3.up;
                }
            }
            else
            {
                float deltaZ = prevBlockPosition.z - lastPosition.z;
                bool isNegativeNum = deltaZ < 0 ? true : false;

                deltaZ = Mathf.Abs(deltaZ);
                if (deltaZ > ErrorMargin)
                {
                    stackBounds.y -= deltaZ;
                    if (stackBounds.y <= 0)
                        return false;

                    float middle = (prevBlockPosition.z + lastPosition.z) / 2;
                    lastBlock.localScale = new Vector3(stackBounds.x, 1, stackBounds.y);

                    Vector3 tempPosition = lastBlock.localPosition;
                    tempPosition.z = middle;
                    lastBlock.localPosition = lastPosition = tempPosition;

                    float rubbleHalfScale = deltaZ / 2;
                    CreateRubble(new
                                     Vector3(lastPosition.x,
                                             lastPosition.y,
                                             isNegativeNum
                                                 ? lastPosition.z + stackBounds.y / 2 + rubbleHalfScale
                                                 : lastPosition.z - stackBounds.y / 2 - rubbleHalfScale),
                                 new Vector3(stackBounds.x, 1, deltaZ));

                    Combo = 0;
                }
                else
                {
                    CheckCombo();
                    lastBlock.localPosition = prevBlockPosition + Vector3.up;
                }
            }

            secondaryPosition = isMovingX ? lastBlock.localPosition.x : lastBlock.localPosition.z;
            return true;
        }

        private void CreateRubble(Vector3 pos, Vector3 scale)
        {
            Cube cube = PoolManager.Instance.Spawn<Cube>(CubeKey);
            cube.CubeRenderer.material.color = lastColor;
            cube.transform.parent = transform;

            cube.transform.localPosition = pos;
            cube.transform.localScale = scale;
            cube.transform.localRotation = Quaternion.identity;

            cube.CubeRigidbody.isKinematic = false;
            cube.name = "Rubble";
        }

        private void CheckCombo()
        {
            Combo++;
            StackUIPresenter.Instance.UpdateCurrentCombo(Combo);

            if (Combo > MaxCombo)
            {
                MaxCombo = Combo;
                StackUIPresenter.Instance.UpdateMaxCombo(MaxCombo);
            }

            if (Combo % 5 == 0)
            {
                stackBounds += new Vector3(0.5f, 0.5f);
                stackBounds.x = stackBounds.x > BoundSize ? BoundSize : stackBounds.x;
                stackBounds.y = stackBounds.y > BoundSize ? BoundSize : stackBounds.y;
            }
        }

        private void UpdateScore()
        {
            if (BestScore >= stackCount) return;

            BestScore = stackCount + 10;
            BestCombo = MaxCombo;
            PlayerPrefs.SetInt(BestScoreKey, BestScore);
            PlayerPrefs.SetInt(BestComboKey, BestCombo);
        }

        private void GameOver()
        {
            isGameOver = true;
            int childCount = transform.childCount;

            for (int i = 0; i < 20; i++)
            {
                if (childCount < i) break;

                var cube = transform.GetChild(i - 1).GetComponent<Cube>();
                if (cube.name.Equals("Rubble")) continue;
                
                cube.CubeRigidbody.isKinematic = false;
                cube.CubeRigidbody.AddForce(Vector3.up * Random.Range(0, 10f) + Vector3.right * (Random.Range(0, 10f) - 5f) * 100f);
            }
            
            UpdateScore();
            StackUIPresenter.Instance.GameOver();
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
            lastColor = applyColor;
            var mainCam = TheStackSceneBase.Instance.MainCamera;
            mainCam.backgroundColor = applyColor - new Color(0.1f, 0.1f, 0.1f);
            StackUIPresenter.Instance.ChangeTextColor(applyColor);

            if (applyColor.Equals(nextColor))
            {
                prevColor = nextColor;
                nextColor = GetRandomColor();
            }
        }
    }
}