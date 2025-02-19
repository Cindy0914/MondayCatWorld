using MondayCatWorld.Character;
using MondayCatWorld.Data;
using MondayCatWorld.Managers;
using MondayCatWorld.UI;
using MondayCatWorld.Utils;
using UnityEngine;

namespace MondayCatWorld.SceneBase
{
    public class LobbySceneBase : SceneSingleton<LobbySceneBase>
    {
        // Object
        [SerializeField] private Camera mainCamera;
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private InteractiveObject theStackCristal;

        // Data
        [SerializeField] private ModelData playerModelData;
        public ModelData PlayerModelData => playerModelData;

        private void Start()
        {
            SetPlayer();
            SceneLoader.Instance.SetCurrentScene(Define.Scene.Lobby);
            SceneLoader.Instance.Init();
            LobbyUIPresenter.Instance.Init();
            theStackCristal.AddInteractEvent(TheStackCristalInteract);
        }

        private void SetPlayer()
        {
            var modelNum = PlayerPrefs.GetInt(Define.ModelNumKey, 5);
            var model = playerModelData.ModelPrefabs[modelNum];

            var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
            player.Tr.position = playerSpawnPoint.position;
            player.SetModel(model);

            GameManager.Instance.SetPlayer(player);
            GameManager.Instance.SetModelIndex(modelNum);
            GameManager.Instance.SetCamera(mainCamera);
        }

        private void TheStackCristalInteract()
        {
            GameManager.Instance.LoadTheStackScene();
        }

        public GameObject GetModelPrefab(int modelNum)
        {
            return playerModelData.ModelPrefabs[modelNum];
        }

        public Sprite GetProfileImage(int modelNum)
        {
            return playerModelData.ModelSprites[modelNum];
        }
    }
}
