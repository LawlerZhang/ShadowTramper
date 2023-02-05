using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    [RequireComponent(typeof(PropsFall))]
    [RequireComponent(typeof(Collider2D))]
    public class Pickable : MonoBehaviour
    {
        protected PlayerController m_player;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                m_player = collision.GetComponent<PlayerController>();
                Effect();
                Destroy(gameObject);
                if (GetComponent<PropsFall>().shadow != null)
                    Destroy(GetComponent<PropsFall>().shadow.gameObject);
            }
        }

        protected virtual void Effect()
        {

        }
    }
}
