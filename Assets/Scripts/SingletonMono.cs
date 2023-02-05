using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Lfish.Pattern
{
    public class SingletonMono<T> : MonoBehaviour where T : Component
    {
        public bool destroyLoadScene = true;
        protected static T m_instance = null;

        public static T Instance
        {
            get
            {
                if (m_instance == null)
                    Create(typeof(T).Name);
                return m_instance;
            }
        }

        public static bool hasInstance => m_instance != null;

        private static void Create(string name)
        {
            if (m_instance == null)
            {
                m_instance = new GameObject().AddComponent<T>();
                m_instance.gameObject.name = name;
            }
        }

        protected void Awake()
        {
            BeforeBaseInit();
            if (Application.isPlaying && !destroyLoadScene && transform.parent == null)
                DontDestroyOnLoad(gameObject);

            //  场景中只有一个物体能挂上该Component
            if (FindObjectsOfType<T>().Length > 1)
            {
                DestroyImmediate(GetComponent<T>());
            }
        }

        protected virtual void BeforeBaseInit() { }

        public void DelayDo(UnityAction action, float time)
        {
            StartCoroutine(DelayDoCoroutine(action, time));
        }

        public void LoopDo(UnityAction action, float interval, float duration = -1)
        {
            StartCoroutine(LoopDoCoroutine(action, interval, duration));
        }

        protected IEnumerator DelayDoCoroutine(UnityAction action, float time)
        {
            yield return new WaitForSecondsRealtime(time);
            action?.Invoke();
        }

        protected IEnumerator LoopDoCoroutine(UnityAction action, float interval, float duration = -1f)
        {
            float nowTime = 0;
            if (duration < 0)
            {
                while (true)
                {
                    yield return new WaitForSecondsRealtime(interval);
                    action?.Invoke();
                }
            }
            else
            {
                while (nowTime < duration)
                {
                    yield return new WaitForSecondsRealtime(interval);
                    nowTime += interval;
                    action?.Invoke();
                }
            }
        }

        protected void OnDestroy()
        {
            m_instance = null;
        }
    }
}
