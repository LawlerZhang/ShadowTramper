using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lfish.Pattern;

namespace Lfish
{
    public class PlayersManager : SingletonMono<PlayersManager>
    {
        public PlayerController redPlayer => m_player0;
        [SerializeField]
        private PlayerController m_player0;
        public PlayerController bluePlayer => m_player1;
        [SerializeField]
        private PlayerController m_player1;

        private new void Awake()
        {
            base.Awake();
        }

        protected override void BeforeBaseInit()
        {
            m_instance = this;
        }

        private void Update()
        {
            if (m_player0.transform.position.y >= m_player1.transform.position.y)
            {
                m_player0.GetComponent<SpriteRenderer>().sortingOrder = 1;
                m_player1.GetComponent<SpriteRenderer>().sortingOrder = 2;
            }
            else
            {
                m_player0.GetComponent<SpriteRenderer>().sortingOrder = 2;
                m_player1.GetComponent<SpriteRenderer>().sortingOrder = 1;
            }
        }

        public PlayerController GetAnotherPlayer(PlayerController player)
        {
            if (player.playerType == PlayerController.Player.Player1)
                return bluePlayer;
            else if (player.playerType == PlayerController.Player.Player2)
                return redPlayer;
            return null;
        }
    }
}
