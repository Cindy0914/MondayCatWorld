using UnityEngine;

namespace MondayCatWorld.Managers
{
    // 해당 씬에서만 사용 할 싱글톤 클래스
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
