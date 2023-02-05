using UnityEngine;

namespace Lfish
{
    public class SceneController : MonoBehaviour
    {
        private void Start()
        {
            Cache();
            GameManager.Instance.Pause();
            ChessData.Instance.ClearData();
            AudioManager.Instance.StartBgm("flirty cha cha", 0.5f);
            GameManager.Instance.DelayDo(() =>
            {
                GameManager.Instance.StartCompetition(PlayersManager.Instance.redPlayer, PlayersManager.Instance.bluePlayer);
            }, 2.0f);
            ChessData.Instance.SetChess(1);
        }

        private void Cache()
        {
            AudioManager.Instance.Cache("ready go");
        }
    }
}
