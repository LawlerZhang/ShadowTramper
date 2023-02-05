using UnityEngine;
using UnityEngine.UI;
using Lfish.Pattern;
using System.Collections;
using Lfish.Manager;

namespace Lfish
{
    [RequireComponent(typeof(MaskableGraphic))]
    public class FadeManager : SingletonMono<FadeManager>
    {
        private MaskableGraphic m_image;
        [SerializeField]
        private float m_halfFadeTime = 0.75f;

        private new void Awake()
        {
            base.Awake();
            m_image = GetComponent<MaskableGraphic>();
        }

        protected override void BeforeBaseInit()
        {
            m_instance = this;
        }

        public void FadeToColor(Color origin, Color target)
        {
            StopAllCoroutines();
            StartCoroutine(FadeToColorCoroutine(origin, target));
        }

        private IEnumerator FadeToColorCoroutine(Color origin, Color target)
        {
            float nowTime = 0.0f;
            float ratio = 0.0f;
            m_image.enabled = true;
            while (ratio < 1)
            {
                nowTime += Time.unscaledDeltaTime;
                ratio = nowTime / m_halfFadeTime;
                m_image.color = Color.Lerp(origin, target, ratio);
                yield return null;
            }
            m_image.enabled = false;
        }

        public void FadeToAlpha(float origin, float target, bool turnBack = false)
        {
            StopAllCoroutines();
            StartCoroutine(FadeToAlphaCoroutine(origin, target, turnBack));
        }

        private IEnumerator FadeToAlphaCoroutine(float origin, float target, bool turnBack = false)
        {
            float nowTime = 0.0f;
            float ratio = 0.0f;
            m_image.enabled = true;
            while (ratio < 1)
            {
                nowTime += Time.unscaledDeltaTime;
                ratio = nowTime / m_halfFadeTime;
                float alpha = Mathf.Lerp(origin, target, ratio);
                m_image.color = new Vector4(m_image.color.r, m_image.color.g, m_image.color.b, alpha);
                yield return null;
            }
            EventManager.Instance.FireEvent(EventTypes.FADE_IN_MIDDLE);
            if (turnBack)
            {
                ratio = 0.0f;
                nowTime = 0.0f;
                while (ratio < 1)
                {
                    nowTime += Time.unscaledDeltaTime;
                    ratio = nowTime / m_halfFadeTime;
                    float alpha = Mathf.Lerp(target, origin, ratio);
                    m_image.color = new Vector4(m_image.color.r, m_image.color.g, m_image.color.b, alpha);
                    yield return null;
                }
            }
            m_image.enabled = false;
            EventManager.Instance.FireEvent(EventTypes.FADE_IN_END);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            destroyLoadScene = true;
        }
#endif 
    }
}
