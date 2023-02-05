using UnityEngine;

namespace Lfish
{
    public class Banana : Pickable
    {
        [SerializeField]
        private float m_slideSpeed = 10.0f;

        protected override void Effect()
        {
            Vector2 nowSpeed = m_player.rigidbody2D.velocity;
            if (nowSpeed.magnitude <= Mathf.Epsilon)
                return;
            m_player.enabled = false;
            m_player.rigidbody2D.velocity = m_slideSpeed * nowSpeed.normalized;
            m_player.animator.SetBool("slide", true);
        }
    }
}
