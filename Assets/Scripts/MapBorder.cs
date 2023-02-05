using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    public class MapBorder : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Player")
            {
                if (collision.collider.GetComponent<PlayerController>().enabled == false)
                {
                    collision.collider.GetComponent<PlayerController>().enabled = true;
                    collision.collider.GetComponent<PlayerController>().animator.SetBool("slide", false);
                }
            }
        }
    }
}
