using System.Collections;
using UnityEngine;


namespace Lfish
{
    public class Light : MonoBehaviour
    {
        private UnityEngine.Rendering.Universal.Light2D m_effect;
        public float breatheDuration
        {
            get => m_breatheDuration;
            set => m_breatheDuration = value;
        }
        [SerializeField]
        [Min(0)]
        private float m_breatheDuration;
        [SerializeField]
        [Min(0)]
        private int m_times;

        private void Awake()
        {
            m_effect = transform.Find("Point Light 2D").GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }

        public void Breathe()
        {
            StartCoroutine(BreatheCoroutine());
        }

        private IEnumerator BreatheCoroutine()
        {
            //if (m_effect.enabled == false)
            //    yield break;
            AudioManager.Instance.PlaySoundEffect("switch light");
            for (int i = 0; i < m_times; ++i)
            {
                float nowBreatheTime = 0f;
                float onceBreatheDuration = m_breatheDuration / m_times;
                while (nowBreatheTime < onceBreatheDuration)
                {
                    nowBreatheTime += Time.deltaTime;
                    if (nowBreatheTime < 0.5f * onceBreatheDuration)
                        m_effect.intensity = 1 - nowBreatheTime / 0.5f / onceBreatheDuration;
                    else
                        m_effect.intensity = (nowBreatheTime - 0.5f * onceBreatheDuration) / 0.5f / onceBreatheDuration;
                    yield return 0;
                }
            }
        }

        public void SetActive(bool active)
        {
            if (this != null)
                m_effect.enabled = active;
        }
    }
}
