using UnityEngine;

namespace Lfish
{
    public class Magnet : Pickable
    {
        [SerializeField]
        [Min(0)]
        private float m_magnetForce;
        [SerializeField]
        [Min(0)]
        private float m_effectTime;
        protected override void Effect()
        {
            PlayerController anotherController = PlayersManager.Instance.GetAnotherPlayer(m_player);
            anotherController.Mute();
            GameManager.Instance.LoopDo(() =>
            {
                anotherController.rigidbody2D.velocity = m_magnetForce * (m_player.transform.position - anotherController.transform.position).normalized;
            }, 0.02f, m_effectTime);
            GameManager.Instance.DelayDo(() =>
            {
                if (anotherController != null)
                {
                    anotherController.Wake();
                    anotherController.rigidbody2D.velocity = Vector2.zero;
                }
            }, m_effectTime + 0.1f);
        }
    }
}
