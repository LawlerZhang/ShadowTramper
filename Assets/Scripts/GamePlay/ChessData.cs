using Lfish.Pattern;
using UnityEngine;
using UnityEngine.UI;

namespace Lfish
{
    public class ChessData : SingletonCommon<ChessData>
    {
        private const int WinScore = 3;
        public int redScore => m_redScore;
        private int m_redScore = 0;
        public int blueScore => m_blueScore;
        private int m_blueScore = 0;

        private int m_chessNumber = 1;

        public void Score(int playerIndex)
        {
            switch (m_chessNumber)
            {
                case 1: // 第一关
                    PlayersManager.Instance.redPlayer.Mute();
                    PlayersManager.Instance.bluePlayer.Mute();
                    if (playerIndex == 0)
                    {
                        ++m_redScore;
                        GameManager.Instance.DelayDo(() =>
                        {
                            PlayersManager.Instance.redPlayer.animator.SetTrigger(Animator.StringToHash("win"));
                            PlayersManager.Instance.redPlayer.Mute();
                            GameManager.Instance.Pause();
                        }, 2.0f);
                    }
                    else if (playerIndex == 1)
                    {
                        ++m_blueScore;
                        GameManager.Instance.DelayDo(() =>
                        {
                            PlayersManager.Instance.bluePlayer.animator.SetTrigger(Animator.StringToHash("win"));
                            PlayersManager.Instance.bluePlayer.Mute();
                            GameManager.Instance.Pause();
                        }, 2.0f);
                    }
                    if (m_redScore <= WinScore && m_blueScore <= WinScore)
                        UiManager.Instance.Play("Score", 2.0f);
                    GameManager.Instance.DelayDo(() =>
                    {
                        CheckWin1();
                    }, 2.1f);
                    break;
                case 2: // 第二关
                    if (playerIndex == 0)
                    {
                        ++m_redScore;
                        AudioManager.Instance.PlaySoundEffect("scoreUp");
                        GameObject.Find("Canvas/Score_Red/Text").GetComponent<Text>().text = m_redScore.ToString();
                    }
                    else if (playerIndex == 1)
                    {
                        ++m_blueScore;
                        AudioManager.Instance.PlaySoundEffect("scoreUp");
                        GameObject.Find("Canvas/Score_Blue/Text").GetComponent<Text>().text = m_blueScore.ToString();
                    }
                    //GameManager.Instance.Pause();
                    //GameManager.Instance.DelayDo(() =>
                    //{
                    //    GameManager.Instance.Continue();
                    //}, 1.0f);
                    break;
            }
        }

        public void SetChess(int number)
        {
            m_chessNumber = number;
        }

        public void ClearData()
        {
            m_redScore = 0;
            m_blueScore = 0;
        }

        public void CheckWin(int chessNumber)
        {
            switch(chessNumber)
            {
                case 1:
                    CheckWin1();
                    break;
                case 2:
                    CheckWin2();
                    break;
            }
        }

        private void CheckWin1()
        {
            if (m_redScore >= WinScore)
            {
                UiManager.Instance.Play("RedVictory", -1);
            }
            else if (m_blueScore >= WinScore)
            {
                UiManager.Instance.Play("BlueVictory", -1);
            }
            else
            {
                GameManager.Instance.DelayDo(() =>
                {
                    GameManager.Instance.RestartCompetition(PlayersManager.Instance.redPlayer, PlayersManager.Instance.bluePlayer);
                }, 2.0f);
            }
        }

        private void CheckWin2()
        {
            PlayersManager.Instance.redPlayer.Mute();
            PlayersManager.Instance.bluePlayer.Mute();
            if (m_redScore > m_blueScore)
                UiManager.Instance.Play("RedVictory", -1);
            else if (m_blueScore > m_redScore)
                UiManager.Instance.Play("BlueVictory", -1);
            else
                UiManager.Instance.Play("ChessTie", -1);
        }
    }
}
