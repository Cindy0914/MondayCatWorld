using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
public class ModelData : ScriptableObject
{
    public List<GameObject> ModelPrefabs;
    public List<Sprite> ModelSprites;
}