using UnityEngine;
using UnityEngine.SceneManagement;

namespace Lfish
{
    public class Victory : MonoBehaviour
    {
        private bool m_canPress = false;

        private void OnEnable()
        {
            GameManager.Instance.DelayDo(() =>
            {
                m_canPress = true;
            }, 3.0f);
        }

        private void Update()
        {
            if (m_canPress && Input.anyKey)
            {
                SceneManager.LoadScene(1);
            }
        }
    }
}
