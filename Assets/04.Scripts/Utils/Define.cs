using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MondayCatWorld.Utils
{
    public static class Define
    {
        // PlayerPref Key
        public const string NameKey = "PlayerName";
        public const string ModelNumKey = "ModelNum";
        public const string PetNumKey = "PetModelNum";
        public const string PetPurchasedKey = "PetPurchased";
        public const string PointKey = "Point";
        public const string BestScoreKey = "BestScore";
        public const string BestComboKey = "BestCombo";
        
        // Pool Key
        public const string CubeKey = "Cube";
        
        public enum Scene
        {
            Title,
            Lobby,
            TheStack,
        }
    }
}