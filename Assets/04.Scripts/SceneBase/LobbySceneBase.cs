using System.Collections.Generic;
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
        [SerializeField] private PetDatas petDatas;

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
            int petNum = PlayerPrefs.GetInt(Define.PetNumKey, -1);
            var point = PlayerPrefs.GetInt(Define.PointKey, 0);

            var player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
            player.Tr.position = playerSpawnPoint.position;
            player.SetModel(model);
            SetPet(player, petNum);

            GameManager.Instance.SetPlayer(player);
            GameManager.Instance.LoadPetPurchaseData(petDatas.PetDataList.Count);
            GameManager.Instance.SetModelIndex(modelNum);
            GameManager.Instance.SetPoint(point);
            GameManager.Instance.SetCamera(mainCamera);
        }

        private void SetPet(Player player, int petNum)
        {
            if (petNum == -1) return;
            
            var petData = petDatas.PetDataList[petNum];
            player.Pet.SetModel(petData);
            player.Pet.gameObject.SetActive(true);
        }
        
        private void TheStackCristalInteract()
        {
            GameManager.Instance.LoadTheStackScene();
        }
        
        public int GetPlayerModelCount()
        {
            return playerModelData.ModelPrefabs.Count;
        }

        public int GetPetModelCount()
        {
            return petDatas.PetDataList.Count;
        }

        public GameObject GetModelPrefab(int modelNum)
        {
            return playerModelData.ModelPrefabs[modelNum];
        }

        public Sprite GetProfileImage(int modelNum)
        {
            return playerModelData.ModelSprites[modelNum];
        }

        public List<PetData> GetPetDataList()
        {
            return petDatas.PetDataList;
        }

        public void LoadPetPurchaseData()
        {
            var purchaseData = GameManager.Instance.PetPurchaseData;
            for (int i = 0; i < purchaseData.Length; i++)
            {
                petDatas.PetDataList[i].IsPurchased = purchaseData[i];
            }
        }
    }
}
