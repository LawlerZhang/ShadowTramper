using UnityEngine;
using UnityEngine.SceneManagement;
using Lfish.Pattern;
using System.Collections.Generic;
using Lfish.Manager;

namespace Lfish
{
    public class GameManager : SingletonMono<GameManager>
    {
        public string sceneNeedToLoad;

        private bool m_popStory = true;  //每当第一次打开游戏，都会弹出背景文字
        private string m_nowSceneName;

        private List<Vector3> m_playerPositions = new List<Vector3>();

        public bool PopStory
        {
            set
            {
                m_popStory = value;
            }
            get
            {
                return m_popStory;
            }
        }

        private new void Awake()
        {
            base.Awake();
            if (string.IsNullOrEmpty(m_nowSceneName))
                m_nowSceneName = SceneManager.GetActiveScene().name;
        }

        protected override void BeforeBaseInit()
        {
            destroyLoadScene = false;
        }

        private void OnEnable()
        {
            //EventManager.Instance.StartListener(EventTypes.PLAYER_DIE, OnDie);
        }

        private void OnDisable()
        {
            //EventManager.Instance.StopListener(EventTypes.PLAYER_DIE, OnDie);
            Time.timeScale = 1;
        }

        private void OnDie(object obj)
        {
            DelayDo(Fail, 2.0f);
        }

        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Continue()
        {
            Time.timeScale = 1;
        }

        public void Fail()
        {
            //AudioManager.Instance.PlaySoundEffect("fail_se");
        }

        public void StartScene()
        {

        }

        public void LoadScene(string sceneName, float delay = 2.0f)
        {
            //FadeManager.Instance.FadeOut();
            DelayDo(() =>
            {
                SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
                m_nowSceneName = sceneName;
            }, delay);
        }
        public void ReloadScene()
        {
            LoadScene(m_nowSceneName);
            Time.timeScale = 1;
        }

        public void StartCompetition(params PlayerController[] players)
        {
            Pause();
            m_playerPositions.Clear();
            foreach (PlayerController player in players)
            {
                m_playerPositions.Add(player.transform.position);
            }
            UiManager.Instance.Play("ReadyGo", 5.0f);
            DelayDo(() =>
            {
                Continue();
            }, 1.7f);
        }

        public void RestartCompetition(params PlayerController[] players)
        {
            EventManager.Instance.StartListener(EventTypes.FADE_IN_MIDDLE, _ =>
            {
                for (int i = 0; i < players.Length; ++i)
                {
                    players[i].transform.position = m_playerPositions[i];
                    players[i].ResetPlayer();
                }
                EventManager.Instance.StopListener(EventTypes.FADE_IN_MIDDLE);
            });
            EventManager.Instance.StartListener(EventTypes.FADE_IN_END, _ =>
            {
                EventManager.Instance.StopListener(EventTypes.FADE_IN_END);
                GameManager.Instance.Continue();
            });
            FadeManager.Instance.FadeToAlpha(0, 1, true);
        }
    }
}
