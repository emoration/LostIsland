using UnityEngine;

namespace Utils
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        // 线程安全的访问接口
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 查找现有实例
                    _instance = FindFirstObjectByType<T>();

                    if (_instance == null)
                    {
                        // 如果实例不存在，创建新的 GameObject 并附加单例组件
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();

                        // 防止销毁
                        DontDestroyOnLoad(singletonObject);
                    }
                }

                return _instance;
            }
        }

        protected virtual void Awake()
        {
            // 确保单例的唯一性
            if (_instance == null)
            {
                _instance = this as T;
                // DontDestroyOnLoad(gameObject); // 保持跨场景存在
            }
            else if (_instance != this)
            {
                Debug.LogWarning("单例模式只能有一个实例！，销毁新实例：" + gameObject.name);
                Destroy(gameObject); // 避免重复单例
            }
        }
    }
}