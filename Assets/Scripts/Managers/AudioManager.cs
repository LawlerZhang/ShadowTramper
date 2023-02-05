using Lfish.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioManager : SingletonMono<AudioManager>
    {
        private AudioSource m_audioSource;

        private Dictionary<string, AudioClip> m_audioClipDict = new Dictionary<string, AudioClip>();

        private new void Awake()
        {
            base.Awake();
            m_audioSource = GetComponent<AudioSource>();
            m_audioSource.loop = true;
        }

        protected override void BeforeBaseInit()
        {
            destroyLoadScene = false;
        }

        // Use this method to play bgm
        public void StartBgm(string audioName, float volume = 1f)
        {
            AudioClip audioClip = null;
            m_nowBgm = audioName;
            if (!m_audioClipDict.ContainsKey(audioName))
            {
                ResourceManager.Instance.LoadAssetAsyn<AudioClip>(audioName, result =>
                {
                    audioClip = result;
                    if (!m_audioClipDict.ContainsKey(audioName))
                        m_audioClipDict.Add(audioName, audioClip);
                    m_audioSource.clip = audioClip;
                    m_audioSource.Play();
                });
            }
            else
            {
                audioClip = m_audioClipDict[audioName];
                m_audioSource.clip = audioClip;
                m_audioSource.Play();
            }
            m_audioSource.volume = volume;
        }

        public string nowBgm => m_nowBgm;
        private string m_nowBgm;

        // Use this method to play Sound Effects
        public void PlaySoundEffect(string audioName)
        {
            AudioClip audioClip = null;
            if (!m_audioClipDict.ContainsKey(audioName))
            {
                ResourceManager.Instance.LoadAssetAsyn<AudioClip>(audioName, result =>
                {
                    audioClip = result;
                    if (!m_audioClipDict.ContainsKey(audioName))
                        m_audioClipDict.Add(audioName, audioClip);
                    GenerateSoundEffect(audioName, audioClip);
                });
            }
            else
            {
                audioClip = m_audioClipDict[audioName];
                GenerateSoundEffect(audioName, audioClip);
            }
        }

        public void StopBgm()
        {
            if (m_audioSource != null)
            {
                m_audioSource.Stop();
            }
        }

        //  预先缓存声音
        public void Cache(string audioName)
        {
            if (!m_audioClipDict.ContainsKey(audioName))
            {
                ResourceManager.Instance.LoadAssetAsyn<AudioClip>(audioName, result =>
                {
                    var audioClip = result;
                    m_audioClipDict.Add(audioClip.name, audioClip);
                });
            }
        }

        //IEnumerator GameObjectDie(GameObject dyingGameObject, float timeToDie)
        //{
        //    yield return new WaitForSecondsRealtime(timeToDie);
        //    Destroy(dyingGameObject);
        //}

        private void GenerateSoundEffect(string audioName, AudioClip audioClip)
        {
            GameObject temp = new GameObject();
            temp.name = audioName;
            AudioSource audioSource = temp.AddComponent<AudioSource>();
            audioSource.clip = audioClip;
            audioSource.Play();
            DontDestroyOnLoad(temp);
            GameManager.Instance.DelayDo(() =>
            {
                if (this != null)
                    Destroy(temp);
            }, audioClip.length);
        }
    }
}
