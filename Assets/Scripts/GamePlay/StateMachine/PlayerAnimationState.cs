using UnityEngine;

namespace Lfish
{
    public class PlayerAnimationState : StateMachineBehaviour
    {
        [System.Serializable]
        public class AudioInfo
        {
            public string stateName;
            public string audioName;
        }
        [SerializeField]
        private AudioInfo[] m_audios;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.GetComponent<PlayerController>().state = animatorStateInfo.shortNameHash;
            foreach (var item in m_audios)
            {
                if (Animator.StringToHash(item.stateName) == animatorStateInfo.shortNameHash)
                    AudioManager.Instance.PlaySoundEffect(item.audioName);
            }
        }
    }
}
