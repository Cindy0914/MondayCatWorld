using System.Collections.Generic;
using UnityEngine;

namespace MondayCatWorld.Data
{
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable/CharacterData")]
    public class ModelData : ScriptableObject
    {
        public List<GameObject> ModelPrefabs;
        public List<Sprite> ModelSprites;
    }
}