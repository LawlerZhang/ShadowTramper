using System.Collections;
using UnityEngine;

namespace Lfish
{
    [RequireComponent(typeof(Camera))]
    public class CameraShake : MonoBehaviour
    {
        private Vector3 m_originPosition;
        private Vector3 m_currentPosition;
        [SerializeField]
        private float m_shakeCd = 0.002f;
        [SerializeField]
        private int m_shakeCount = 60;
        private int m_nowShakedCount = 0;
        private float m_shakedTime;
        [SerializeField]
        [Min(0.0f)]
        private float m_scope;

        private new Camera camera => GetComponent<Camera>();

        private void Awake()
        {
            m_originPosition = transform.position;
            m_currentPosition = transform.position;
        }

        public void Shake()
        {
            StartCoroutine(ShakeCoroutine());
        }

        private IEnumerator ShakeCoroutine()
        {
            m_nowShakedCount = 0;
            m_shakedTime = m_shakeCd;
            while (m_nowShakedCount < m_shakeCount)
            {
                m_shakedTime += Time.deltaTime;
                if (m_shakedTime >= m_shakeCd)
                {
                    m_currentPosition = m_originPosition + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0) * m_scope;
                    camera.transform.position = m_currentPosition;
                    m_shakedTime -= m_shakeCd;
                    ++m_nowShakedCount;
                }
                yield return null;
            }
            m_currentPosition = m_originPosition;
            camera.transform.position = m_currentPosition;
        }
    }
}
