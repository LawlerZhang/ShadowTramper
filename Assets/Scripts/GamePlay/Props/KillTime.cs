using Lfish.Manager;
using UnityEngine;

namespace Lfish
{
    public class KillTime : Pickable
    {
        public Sprite falledSprite;
        private bool m_started = false;
        private void OnEnable()
        {
            EventManager.Instance.StartListener(EventTypes.FALL_END, OnFallEnd);
        }

        private void OnDisable()
        {
            EventManager.Instance.StopListener(EventTypes.FALL_END, OnFallEnd);
        }

        private void OnFallEnd(object obj)
        {
            GetComponent<SpriteRenderer>().sprite = falledSprite;
            if (!m_started)
            {
                KillTimeManager.Instance.StartKill();
                m_started = true;
            }
            GameManager.Instance.DelayDo(() =>
            {
                if (this != null)
                {
                    Destroy(gameObject);
                    if (GetComponent<PropsFall>().shadow != null)
                        Destroy(GetComponent<PropsFall>().shadow.gameObject);
                }
            }, 1.0f);
        }

        protected override void Effect()
        {
            if (!m_started)
            {
                KillTimeManager.Instance.StartKill();
                m_started = true;
            }
        }
    }
}
