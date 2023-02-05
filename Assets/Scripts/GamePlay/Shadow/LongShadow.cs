using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    public class LongShadow : MonoBehaviour
    {
        private float m_originMoveSpeed;
        private PlayerController m_parent;
        private void Awake()
        {
            m_parent = transform.parent.parent.GetComponent<PlayerController>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                m_originMoveSpeed = m_parent.moveSpeed;
                m_parent.moveSpeed *= 0.5f;
                collision.GetComponent<PlayerController>().targetPlayer = m_parent;
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                m_parent.moveSpeed = m_originMoveSpeed;
                collision.GetComponent<PlayerController>().targetPlayer = null;
            }
        }
    }
}
