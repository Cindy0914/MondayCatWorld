using UnityEngine;

namespace MondayCatWorld.Managers
{
    public class SceneSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T instance;

        public static T Instance => instance ? instance : null;

        public void Awake()
        {
            instance = this as T;
        }

        public void OnDestroy()
        {
            instance = null;
        }
    }
}
