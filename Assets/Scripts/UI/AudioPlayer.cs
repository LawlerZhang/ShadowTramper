using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    public class AudioPlayer : MonoBehaviour
    {
        public enum TriggerType
        {
            OnEnable,
            OnDisable,
            OnDestroy
        }
        [SerializeField]
        private TriggerType m_triggerType = TriggerType.OnEnable;
        public enum AudioType
        {
            BGM,
            SE
        }
        [SerializeField]
        private AudioType m_audioType;
        [SerializeField]
        private string m_audioName;
        [SerializeField]
        [Min(0f)]
        private float m_offset;

        public void OnEnable()
        {
            if (m_triggerType != TriggerType.OnEnable)
                return;
            Play();
        }

        public void OnDisable()
        {
            if (m_triggerType != TriggerType.OnDisable)
                return;
            Play();
        }

        public void OnDestroy()
        {
            if (m_triggerType != TriggerType.OnDestroy)
                return;
            Play();
        }

        private void Play()
        {
            AudioManager.Instance.Cache(m_audioName);
            GameManager.Instance.DelayDo(() =>
            {
                if (m_audioType == AudioType.BGM)
                    AudioManager.Instance.StartBgm(m_audioName);
                else if (m_audioType == AudioType.SE)
                    AudioManager.Instance.PlaySoundEffect(m_audioName);
            }, m_offset);
        }
    }
}
