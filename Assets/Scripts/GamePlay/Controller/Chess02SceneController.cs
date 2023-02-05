using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    public class Chess02SceneController : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.StartBgm("classroom");
            GameManager.Instance.Pause();
            ChessData.Instance.ClearData();
            GameManager.Instance.DelayDo(() =>
            {
                GameManager.Instance.StartCompetition(PlayersManager.Instance.redPlayer, PlayersManager.Instance.bluePlayer);
                TimeCountDown.Instance.StartCount();
            }, 2.0f);
            ChessData.Instance.SetChess(2);
        }
    }
}
