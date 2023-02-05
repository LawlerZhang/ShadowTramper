using Lfish.Manager;
using System.Collections;
using UnityEngine;

namespace Lfish
{
    public class PropsFall : MonoBehaviour
    {
        public Transform shadow => m_target;
        private Transform m_target;

        [SerializeField]
        private float m_fallNeedTime;
        public void StartFall(Transform target)
        {
            gameObject.SetActive(true);
            m_target = target;
            StartCoroutine(FallCoroutine());
        }

        private IEnumerator FallCoroutine()
        {
            transform.position = m_target.position + new Vector3(0, 3f, 0);
            Vector3 originPosition = transform.position;
            Vector3 targetPosition = m_target.position + new Vector3(0, 0.28f, 0);
            float nowFallTime = 0f;
            while (nowFallTime < m_fallNeedTime)
            {
                float ratio = nowFallTime / m_fallNeedTime;
                float scale = 0.8f + ratio * 0.2f;
                transform.localScale = new Vector3(scale, scale, 1);
                m_target.localScale = new Vector3(scale, scale, 1);
                transform.position = Vector3.Lerp(originPosition, targetPosition, ratio);
                nowFallTime += Time.deltaTime;
                yield return null;
            }
            transform.position = targetPosition;
            EventManager.Instance.FireEvent(EventTypes.FALL_END);
        }
    }
}
