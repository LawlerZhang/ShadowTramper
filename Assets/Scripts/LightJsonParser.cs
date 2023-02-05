using LitJson;
using System.Collections;
using Lfish.Pattern;
using System.Text;
using UnityEngine.Events;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Lfish
{
    public class LightJsonParser : SingletonMono<LightJsonParser>
    {
        private new void Awake()
        {
            base.Awake();
            if (m_instance == null)
                m_instance = this;
        }

        protected override void BeforeBaseInit()
        {
            destroyLoadScene = false;
        }

        public void Parse(string path, UnityAction<List<LightData>> onParseCompleted)
        {
            StartCoroutine(ParseCoroutine(path, onParseCompleted));
        }

        private IEnumerator ParseCoroutine(string path, UnityAction<List<LightData>> onParseCompleted)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(path))
            {
                yield return webRequest.SendWebRequest();

                string jsonString = Encoding.UTF8.GetString(webRequest.downloadHandler.data);
                List<LightData> data = JsonMapper.ToObject<List<LightData>>(jsonString);
                if (onParseCompleted != null)
                    onParseCompleted.Invoke(data);
            }
        }
    }
}
