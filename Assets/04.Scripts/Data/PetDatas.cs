using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PetDatas", menuName = "Scriptable/PetDatas")]
// 펫 데이터 리스트를 들고 있는 ScriptableObject 클래스
public class PetDatas : ScriptableObject
{
    public List<PetData> PetDataList;
}

[Serializable]
public class PetData
{
    public GameObject Prefab;
    public Sprite Sprite;
    public string Name;
    public int Price;
    public float FollowSpeed;
    public int IsPurchased; // 0: Not Purchased, 1: Purchased
}

[Serializable]
// 구매 정보 배열을 PlayerPref에 저장하기 위한 Wrapper 클래스
public class PurchaseDataWrapper
{
    public PurchaseDataWrapper(int[] purchaseData)
    {
        PurchaseData = purchaseData;
    }
    
    public int[] PurchaseData;
}
