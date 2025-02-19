using System.Collections.Generic;
using UnityEngine;

namespace MondayCatWorld.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
    public class ModelData : ScriptableObject
    {
        public List<GameObject> ModelPrefabs;
        public List<Sprite> ModelSprites;
    }
}