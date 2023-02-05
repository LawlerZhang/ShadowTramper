using Lfish.Pattern;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    public class UiManager : SingletonMono<UiManager>
    {
        private Transform m_canvas;
        private Dictionary<string, GameObject> m_uiCache = new Dictionary<string, GameObject>();
        private new void Awake()
        {
            m_canvas = GameObject.Find("Canvas")?.transform;
        }

        /// <summary>
        /// 加载UI
        /// </summary>
        /// <param name="uiName"></param>
        /// <param name="duration">负数表示永远存在</param>
        /// <param name="sibingIndex"></param>
        public void Play(string uiName, float duration)
        {
            if (m_canvas == null)
                m_canvas = GameObject.Find("Canvas")?.transform;
            if (m_uiCache.ContainsKey(uiName))
            {
                m_uiCache[uiName].SetActive(true);
                if (duration > 0)
                {
                    GameManager.Instance.DelayDo(() =>
                    {
                        m_uiCache[uiName].SetActive(false);
                    }, duration);
                }
            }
            else
            {
                ResourceManager.Instance.LoadAssetAsyn<GameObject>(uiName, result =>
                {
                    GameObject go = Instantiate(result, m_canvas, false);
                    //go.transform.SetSiblingIndex(sibingIndex);
                    m_uiCache.Add(uiName, go);
                    if (duration > 0)
                    {
                        GameManager.Instance.DelayDo(() =>
                        {
                            m_uiCache[uiName].SetActive(false);
                        }, duration);
                    }
                });
            }
        }
    }
}
