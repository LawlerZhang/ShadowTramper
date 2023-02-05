using System.Collections.Generic;
using UnityEngine;
using Lfish.Pattern;

namespace Lfish
{
    public class LightController : SingletonMono<LightController>
    {
        public Light[] lights;
        public string jsonFileName;

        private float m_nowTime = 0f;
        private int m_playedIndex = -1;

        public List<LightData> dataList => m_dataList;
        private List<LightData> m_dataList;

        public Light lighten => m_lighten;
        private Light m_lighten;
        private int m_lightIndex;
        private bool m_random = false;
        private float m_randomInterval;
        private float m_nowRandomTime = 0;

        private new void Awake()
        {
            base.Awake();
        }

        protected override void BeforeBaseInit()
        {
            m_instance = this;
        }

        private void Start()
        {
            LightJsonParser.Instance.Parse(Application.streamingAssetsPath + "/Json/" + jsonFileName, OnJsonParsed);
        }

        private void OnJsonParsed(List<LightData> dataList)
        {
            m_dataList = dataList;
            if (m_dataList != null)
            {
                if (!m_dataList[0].isRandom)
                    SetLightImmediatly(m_dataList[0].needToLighten, true);
                else
                {
                    int tempIndex = Random.Range(0, lights.Length);
                    SetLightImmediatly(tempIndex);
                }
            }
        }

        private void Update()
        {
            if (m_dataList == null)
                return;
            UpdateLights();
        }

        private void UpdateLights()
        {
            if (m_playedIndex + 1 < m_dataList.Count && m_nowTime >= m_dataList[m_playedIndex + 1].time)
            {
                m_playedIndex = m_playedIndex + 1;
                m_random = m_dataList[m_playedIndex].isRandom;
                if (!m_random)
                    SetLights();
            }
            if (m_random)
            {
                //if (m_nowRandomTime == 0.0f)
                m_randomInterval = Random.Range((float)m_dataList[m_playedIndex].interval[0], (float)m_dataList[m_playedIndex].interval[1]);
                if (m_nowRandomTime > m_randomInterval)
                {
                    int tempIndex = Random.Range(0, lights.Length);
                    m_lightIndex = tempIndex != m_lightIndex ? tempIndex : Random.Range(0, lights.Length);
                    SetLights(m_lightIndex);
                    m_nowRandomTime = 0;
                }
                m_nowRandomTime += Time.deltaTime;
            }
            m_nowTime += Time.deltaTime;
        }

        private void SetLightImmediatly(int lightIndex, bool increseIndex = false)
        {
            foreach (var item in lights)
            {
                item.SetActive(false);
            }
            lights[lightIndex].SetActive(true);
            m_lighten = lights[lightIndex];
            if (increseIndex)
                ++m_playedIndex;
        }

        private void SetLights()
        {
            m_lighten?.Breathe();
            GameManager.Instance.DelayDo(() =>
            {
                foreach (var item in lights)
                {
                    item.SetActive(false);
                }
                lights[m_dataList[m_playedIndex].needToLighten].SetActive(true);
                m_lighten = lights[m_dataList[m_playedIndex].needToLighten];
            }, 3.4f);
        }

        private void SetLights(int lightenIndex)
        {
            m_lighten?.Breathe();
            GameManager.Instance.DelayDo(() =>
            {
                foreach (var item in lights)
                {
                    if (item != null)
                        item.SetActive(false);
                }

                lights[lightenIndex].SetActive(true);
                m_lighten = lights[lightenIndex];
            }, 3.4f);
        }
    }
}
