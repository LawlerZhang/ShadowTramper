using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lfish.Pattern;

namespace Lfish
{
    public class KillTimeManager : SingletonMono<KillTimeManager>
    {
        private new void Awake()
        {
            base.Awake();
        }

        protected override void BeforeBaseInit()
        {
            m_instance = this;
        }

        private string m_originBgmName;

        public void StartKill()
        {
            m_originBgmName = AudioManager.Instance.nowBgm;
            AudioManager.Instance.StartBgm("kill time");
            foreach (var data in LightController.Instance.dataList)
            {
                data.time *= 0.5f;
                if (data.interval != null)
                {
                    data.interval[0] = 2f;
                    data.interval[1] = 2f;
                }
            }
            foreach (var light in LightController.Instance.lights)
            {
                light.breatheDuration *= 0.5f;
            }

            PlayersManager.Instance.redPlayer.animator.speed = 2.0f;
            PlayersManager.Instance.bluePlayer.animator.speed = 2.0f;
            PlayersManager.Instance.redPlayer.moveSpeed = 8.0f;
            PlayersManager.Instance.bluePlayer.moveSpeed = 8.0f;
        }

        //public void StopKill()
        //{
        //    string temp = AudioManager.Instance.nowBgm;
        //    AudioManager.Instance.StartBgm(m_originBgmName);
        //    m_originBgmName = temp;
        //    foreach (var data in LightController.Instance.dataList)
        //    {
        //        data.time *= 2f;
        //        if (data.interval != null)
        //        {
        //            data.interval[0] *= 2f;
        //            data.interval[1] *= 2f;
        //        }
        //    }
        //    foreach (var light in LightController.Instance.lights)
        //    {
        //        light.breatheDuration *= 2.0f;
        //    }
        //    PlayersManager.Instance.redPlayer.animator.speed = 1.0f;
        //    PlayersManager.Instance.bluePlayer.animator.speed = 1.0f;
        //    PlayersManager.Instance.redPlayer.moveSpeed = 5.0f;
        //    PlayersManager.Instance.bluePlayer.moveSpeed = 5.0f;
        //}
    }
}
