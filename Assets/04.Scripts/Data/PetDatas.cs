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
    public float AddSpeed;
    public bool IsPurchased;
}
