using MondayCatWorld.Character;
using MondayCatWorld.Utils;
using UnityEngine;

namespace MondayCatWorld.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField] private GameObject playerPrefab;
        
        public Player Player { get; private set; }
        public string Nickname { get; private set; }
        public Camera MainCamera { get; private set; }

        private void Start()
        {
            PoolManager.Instance.Init();
            LobbySceneInit();
        }

        private void LobbySceneInit()
        {
            if (Camera.main != null)
                MainCamera = Camera.main;
            else
                Debug.LogError("Main Camera is not found");
            
            var camController = MainCamera.GetComponent<CameraController>();
            camController.SetTarget(Player.transform);
        }
        
        public void SetPlayer(string nickname)
        {
            Nickname = nickname;
            Player = Instantiate(playerPrefab).GetComponent<Player>();
            Nickname = nickname;
        }

        public void LoadTheStackScene()
        {
            SceneLoader.Instance.LoadSceneAsync(Define.Scene.TheStack);
        }
    }
}