using System;
using System.Collections.Generic;
using System.Text;
using MondayCatWorld.Character;
using MondayCatWorld.SceneBase;
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
        public int PetIndex { get; private set; }
        public int[] PetPurchaseData { get; private set; }
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
        
        public void SetPet(int petIndex)
        {
            PetIndex = petIndex;
            PlayerPrefs.SetInt(Define.PetNumKey, PetIndex);
        }

        public void PurchasePet(int index)
        {
            PetPurchaseData[index] = 1;
            SavePetPurchaseData();
        }
        
        public void SavePetPurchaseData()
        {
            var wrapper = new PurchaseDataWrapper(PetPurchaseData);
            var purchased = JsonUtility.ToJson(wrapper);
            PlayerPrefs.SetString(Define.PetPurchasedKey, purchased);
        }
        
        public void LoadPetPurchaseData(int petCount)
        {
            PetPurchaseData = new int[petCount];
            var purchased = PlayerPrefs.GetString(Define.PetPurchasedKey, string.Empty);
            if (purchased == string.Empty)
            {
                return;
            }
            
            var wrapper = JsonUtility.FromJson<PurchaseDataWrapper>(purchased);
            PetPurchaseData = wrapper.PurchaseData;
        }
        
        public void SetPoint(int point)
        {
            Point = point;
            PlayerPrefs.SetInt(Define.PointKey, Point);
        }

        public void AddPoint(int point)
        {
            Point += point;
            PlayerPrefs.SetInt(Define.PointKey, Point);
        }
        
        public void RemovePoint(int point)
        {
            Point -= point;
            PlayerPrefs.SetInt(Define.PointKey, Point);
        }
        
        public void SetModelIndex(int index)
        {
            ModelIndex = index;
            PlayerPrefs.SetInt(Define.ModelNumKey, ModelIndex);
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

        // Debug
        public void OnGUI()
        {
#if UNITY_EDITOR
            GUIStyle myButtonStyle = new GUIStyle(GUI.skin.button)
            {
                fontSize = 30,
                fixedWidth = 300,
                fixedHeight = 80
            };
            
            if (GUILayout.Button("PlayerPrefs DeleteAll", myButtonStyle))
            {
                PlayerPrefs.DeleteAll();
            }

            if (GUILayout.Button("Add Point", myButtonStyle))
            {
                AddPoint(100);
            }
#endif
        }
    }
}

