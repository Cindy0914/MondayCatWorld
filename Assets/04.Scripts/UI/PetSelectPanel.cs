using System.Collections.Generic;
using MondayCatWorld.Managers;
using MondayCatWorld.SceneBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MondayCatWorld.UI
{

    public class PetSelectPanel : MonoBehaviour
    {
        public Image PetImage;
        public TextMeshProUGUI PetName;
        public Button LeftButton;
        public Button RightButton;
        public GameObject PricePanel;
        public TextMeshProUGUI Price;
        public Button ChangeButton;
        public Button PurchaseButton;
        public Button CloseButton;

        private List<PetData> petDataList;
        private int maxIndex;
        private int currentIndex;

        public void Init(int petCount, int currentPet)
        {
            petDataList = LobbySceneBase.Instance.GetPetDataList();
            maxIndex = petCount;
            currentIndex = currentPet;
            RefreshPetData();
            LeftButton.onClick.AddListener(OnLeftButtonClick);
            RightButton.onClick.AddListener(OnRightButtonClick);
            ChangeButton.onClick.AddListener(OnChangeButtonClick);
            PurchaseButton.onClick.AddListener(OnPurchaseButtonClick);
            CloseButton.onClick.AddListener(Close);
        }

        private void OnChangeButtonClick()
        {
            var petData = petDataList[currentIndex];
            GameManager.Instance.Player.Pet.SetModel(petData);
            GameManager.Instance.SetPet(currentIndex);
            Close();
        }

        private void OnPurchaseButtonClick()
        {
            var petData = petDataList[currentIndex];
            if (GameManager.Instance.Point < petData.Price)
                return;
            
            petData.IsPurchased = 1;
            GameManager.Instance.RemovePoint(petData.Price);
            GameManager.Instance.PurchasePet(currentIndex);
            GameManager.Instance.SetPet(currentIndex);
            
            PricePanel.SetActive(false);
            PurchaseButton.gameObject.SetActive(false);
            ChangeButton.gameObject.SetActive(true);
        }

        private void OnLeftButtonClick()
        {
            currentIndex--;
            if (currentIndex < 0)
                currentIndex = maxIndex;

            RefreshPetData();
        }

        private void OnRightButtonClick()
        {
            currentIndex++;
            if (currentIndex > maxIndex)
                currentIndex = 0;
            
            RefreshPetData();
        }

        private void RefreshPetData()
        {
            var petData = petDataList[currentIndex];
            PetImage.sprite = petData.Sprite;
            PetName.text = petData.Name;
            if (petData.IsPurchased == 0)
            {
                Price.text = petData.Price.ToString();
                PricePanel.SetActive(true);
                PurchaseButton.gameObject.SetActive(true);
                ChangeButton.gameObject.SetActive(false);
            }
            else
            {
                PricePanel.SetActive(false);
                PurchaseButton.gameObject.SetActive(false);
                ChangeButton.gameObject.SetActive(true);
            }
        }

        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}