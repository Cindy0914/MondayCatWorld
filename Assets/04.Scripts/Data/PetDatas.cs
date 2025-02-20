using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PetDatas", menuName = "Scriptable/PetDatas")]
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
public class PurchaseDataWrapper
{
    public PurchaseDataWrapper(int[] purchaseData)
    {
        PurchaseData = purchaseData;
    }
    
    public int[] PurchaseData;
}
