using UnityEngine;
using UnityEngine.UI;
using Lfish.Pattern;
using System.Collections;
using System.Collections.Generic;

namespace Lfish
{
    public class TimeCountDown : SingletonMono<TimeCountDown>
    {
        [SerializeField]
        private int m_initTime;
        [SerializeField]
        private Text m_timeText;
        private float m_previousTime;
        private float m_nowTime;

        private int m_nowPropsIndex = -1;

        private new void Awake()
        {
            base.Awake();
            m_nowTime = m_initTime;
            m_previousTime = m_nowTime + 1.1f;
        }

        protected override void BeforeBaseInit()
        {
            m_instance = this;
        }

        public void StartCount()
        {
            StartCoroutine(TimeCountCoroutine());
        }

        private IEnumerator TimeCountCoroutine()
        {
            List<PropsData> propsData = PropsManager.Instance.dataList;
            while (m_nowTime > 0)
            {
                // props fall check
                if (propsData != null && m_nowPropsIndex + 1 < propsData.Count && m_nowTime < propsData[m_nowPropsIndex + 1].time)
                {
                    ++m_nowPropsIndex;
                    if (!propsData[m_nowPropsIndex].isRandom)
                        PropsManager.Instance.Generate(propsData[m_nowPropsIndex].propName);
                    else
                        PropsManager.Instance.Generate(propsData[m_nowPropsIndex].randomProps[Random.Range(0, propsData[m_nowPropsIndex].randomProps.Length)]);
                }
                if (m_previousTime - m_nowTime >= 0.9f)
                {
                    m_timeText.text = ((int)m_nowTime).ToString();
                    m_previousTime = m_nowTime;
                }
                m_nowTime -= Time.deltaTime;
                yield return null;
            }
            ChessData.Instance.CheckWin(2);
        }
    }
}
