using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    public class TitleSceneController : MonoBehaviour
    {
        private void Start()
        {
            AudioManager.Instance.StartBgm("begin");
        }
    }
}
