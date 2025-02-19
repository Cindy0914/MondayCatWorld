using MondayCatWorld.Character;
using MondayCatWorld.Utils;
using UnityEngine;

namespace MondayCatWorld.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public Player Player { get; private set; }
        public string Nickname { get; private set; }
        public int Point { get; private set; }
        public int ModelIndex { get; private set; }
        public Camera MainCamera { get; private set; }

        private void Start()
        {
            PoolManager.Instance.Init();
        }

        public void SetName(string nickname)
        {
            Nickname = nickname;
            PlayerPrefs.SetString("Nickname", Nickname);
        }
        
        public void SetPlayer(Player player)
        {
            Player = player;
        }
        
        public void SetPoint(int point)
        {
            Point = point;
            PlayerPrefs.SetInt("Point", Point);
        }

        public void AddPoint(int point)
        {
            Point += point;
            PlayerPrefs.SetInt("Point", Point);
        }
        
        public void SetModelIndex(int index)
        {
            ModelIndex = index;
            PlayerPrefs.SetInt("ModelIndex", ModelIndex);
        }

        public void SetCamera(Camera camera)
        {
            MainCamera = camera;
            var camController = MainCamera.GetComponent<CameraController>();
            camController.SetTarget(Player.transform);
        }

        public void LoadTheStackScene()
        {
            SceneLoader.Instance.LoadSceneAsync(Define.Scene.TheStack);
        }
        
        public void LoadLobbyScene()
        {
            SceneLoader.Instance.LoadSceneAsync(Define.Scene.Lobby);
        }
    }
}