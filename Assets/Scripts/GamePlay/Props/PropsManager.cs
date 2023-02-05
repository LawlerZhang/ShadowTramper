using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lfish.Pattern;

namespace Lfish
{
    public class PropsManager : SingletonMono<PropsManager>
    {
        public string jsonFileName;
        public List<PropsData> dataList => m_dataList;
        private List<PropsData> m_dataList;

        private new void Awake()
        {
            base.Awake();
            PropsJsonParser.Instance.Parse(Application.streamingAssetsPath + "/Json/" + jsonFileName, OnJsonParsed);
        }

        protected override void BeforeBaseInit()
        {
            m_instance = this;
        }

        private void OnJsonParsed(List<PropsData> dataList)
        {
            m_dataList = dataList;
        }

        public void Generate(string propsName)
        {
            ResourceManager.Instance.LoadAssetAsyn<GameObject>("CommonShadow", shadow =>
            {
                GameObject newShadow = GameObject.Instantiate(shadow, RandomPointInScene.Instance.RandomPosition(), Quaternion.identity);
                ResourceManager.Instance.LoadAssetAsyn<GameObject>(propsName, prop =>
                {
                    GameObject newProp = GameObject.Instantiate(prop, new Vector3(0, 0, 0), Quaternion.identity);
                    newProp.GetComponent<PropsFall>().StartFall(newShadow.transform);
                });
            });


        }
    }
}
