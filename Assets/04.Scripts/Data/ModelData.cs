using System.Collections.Generic;
using UnityEngine;

namespace MondayCatWorld.Data
{
    // 캐릭터의 모델 데이터 리스트를 들고 있는 ScriptableObject 클래스
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Scriptable/CharacterData")]
    public class ModelData : ScriptableObject
    {
        public List<GameObject> ModelPrefabs;
        public List<Sprite> ModelSprites;
    }
}