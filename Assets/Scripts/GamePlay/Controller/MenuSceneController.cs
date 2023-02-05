using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    public class MenuSceneController : MonoBehaviour
    {
        private void Start()
        {
            GameManager.Instance.Continue();
            AudioManager.Instance.StartBgm("flirty cha cha", 0.5f);
        }
    }
}
