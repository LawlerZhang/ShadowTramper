using UnityEngine;
using UnityEngine.UI;

namespace Lfish
{
    public class Score : MonoBehaviour
    {
        [SerializeField]
        private Image m_redScoreImage;
        [SerializeField]
        private Image m_blueScoreImage;
        [SerializeField]
        private Sprite[] m_redScores;
        [SerializeField]
        private Sprite[] m_blueScores;

        private void OnEnable()
        {
            m_redScoreImage.sprite = m_redScores[ChessData.Instance.blueScore];
            m_blueScoreImage.sprite = m_blueScores[ChessData.Instance.redScore];
        }
    }
}
