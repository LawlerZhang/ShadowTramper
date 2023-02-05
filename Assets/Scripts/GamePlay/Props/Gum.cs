using UnityEngine;

namespace Lfish
{

    public class Gum : Pickable
    {
        [SerializeField]
        [Range(0f, 1f)]
        private float m_speedFadeRatio;
        [SerializeField]
        private float m_duration;

        protected override void Effect()
        {
            float originMoveSpeed = m_player.moveSpeed;
            m_player.moveSpeed *= (1 - m_speedFadeRatio);
            GameManager.Instance.DelayDo(() =>
            {
                m_player.moveSpeed = originMoveSpeed;
            }, m_duration);
        }
    }
}
