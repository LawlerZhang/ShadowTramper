using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lfish
{
    [RequireComponent(typeof(Collider2D))]
    public class SelectableElement : MonoBehaviour
    {
        public enum SelectType
        {
            GoToMenu,
            GoToTitle,
            Chess01,
            Chess02,
            Tutorial,
            None
        }
        [SerializeField]
        private SelectType m_type;

        private Sprite m_normalSprite;
        [SerializeField]
        private Sprite m_selectedSprite;

        private new SpriteRenderer renderer => GetComponent<SpriteRenderer>();

        private bool m_redPlayerIn = false;
        private bool m_bluePlayerIn = false;

        private void Awake()
        {
            m_normalSprite = renderer.sprite;
        }

        private void Update()
        {
            renderer.sprite = (m_redPlayerIn || m_bluePlayerIn) ? m_selectedSprite : m_normalSprite;

            if ((m_redPlayerIn && InputManager.Instance.GetStamp(0)) || (m_bluePlayerIn && InputManager.Instance.GetStamp(1)))
            {
                switch (m_type)
                {
                    case SelectType.GoToMenu:
                        SceneManager.LoadSceneAsync(1);
                        break;
                    case SelectType.GoToTitle:
                        if (SceneManager.GetActiveScene().buildIndex == 0)
                        {
                            transform.parent.parent.GetChild(0).gameObject.SetActive(true);
                            transform.parent.gameObject.SetActive(false);
                        }
                        else
                            SceneManager.LoadSceneAsync(0);
                        break;
                    case SelectType.Chess01:
                        SceneManager.LoadSceneAsync(2);
                        break;
                    case SelectType.Chess02:
                        SceneManager.LoadSceneAsync(3);
                        break;
                    case SelectType.Tutorial:
                        transform.parent.parent.GetChild(1).gameObject.SetActive(true);
                        transform.parent.gameObject.SetActive(false);
                        break;
                    case SelectType.None:
                        break;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (!m_redPlayerIn && !m_bluePlayerIn)
                    AudioManager.Instance.PlaySoundEffect("button");
                if (collision.GetComponent<PlayerController>().playerType == PlayerController.Player.Player1)
                    m_redPlayerIn = true;
                else if (collision.GetComponent<PlayerController>().playerType == PlayerController.Player.Player2)
                    m_bluePlayerIn = true;
            }
        }


        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.tag == "Player")
            {
                if (collision.GetComponent<PlayerController>().playerType == PlayerController.Player.Player1)
                    m_redPlayerIn = false;
                else if (collision.GetComponent<PlayerController>().playerType == PlayerController.Player.Player2)
                    m_bluePlayerIn = false;
            }
        }
    }
}
