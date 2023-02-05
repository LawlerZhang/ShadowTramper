using UnityEngine;

namespace Lfish
{
    [RequireComponent(typeof(Rigidbody2D))]
    public partial class PlayerController : MonoBehaviour
    {
        public enum Player
        {
            Player1,
            Player2,
        }
        public Player playerType => m_player;
        [SerializeField]
        private Player m_player;

        [HideInInspector]
        public int state;

        public float moveSpeed
        {
            get => m_moveSpeed;
            set => m_moveSpeed = value;
        }
        [SerializeField]
        [Range(-20, 20)]
        private float m_moveSpeed = 5;
        private bool m_lockMove = false;
        private bool m_flipX = false;
        [SerializeField]
        private float m_moveAudioInerval = 1f;
        private bool m_startRun = false;
        private float m_runTime = 0f;
        [SerializeField]
        private float m_stampCd = 0.5f;
        private float m_nowStampTime = 0f;
        private bool m_mute = false; // Stop all action
        public PlayerController targetPlayer
        {
            get => m_targetPlayer;
            set => m_targetPlayer = value;
        }
        private PlayerController m_targetPlayer = null;

        private Vector2 m_axis;

        private void Update()
        {
            if (m_mute)
                return;
            m_axis = InputManager.Instance.GetAxis((int)m_player);

            // move
            rigidbody2D.velocity = m_lockMove ? Vector2.zero : m_axis * m_moveSpeed;

            // handle animation
            UpdateAnimation();

            // handle shadow
            if (LightController.Instance.lighten != null)
            {
                shadow.GetComponent<SpriteRenderer>().enabled = false;
                shadow.longShadow.GetComponent<SpriteRenderer>().enabled = true;
                shadow.SetRotation(LightController.Instance.lighten.transform);
            }
            else
            {
                shadow.GetComponent<SpriteRenderer>().enabled = true;
                shadow.longShadow.GetComponent<SpriteRenderer>().enabled = false;
                shadow.SetRotation(null);
            }

            // handle run sound
            if (rigidbody2D.velocity.magnitude >= 0.5f)
            {
                if (!m_startRun)
                {
                    m_runTime = m_moveAudioInerval;
                    m_startRun = true;
                }
                m_runTime += Time.deltaTime;
                if (m_runTime > m_moveAudioInerval)
                {
                    AudioManager.Instance.PlaySoundEffect("run on sand");
                    m_runTime = 0;
                }
            }
            else
            {
                m_startRun = false;
                m_runTime = 0;
            }

            // handle stamp
            m_nowStampTime += Time.deltaTime;
            if (m_nowStampTime >= m_stampCd && InputManager.Instance.GetStamp((int)m_player))
            {
                m_nowStampTime = 0;
                Stamp();
            }
        }

        private void UpdateAnimation()
        {
            if (m_axis.x < 0)
                m_flipX = true;
            else if (m_axis.x > 0)
                m_flipX = false;
            render.flipX = m_flipX;

            if (state == Animator.StringToHash("Idle"))
            {
                m_lockMove = false;
                // start run
                if (rigidbody2D.velocity.magnitude > Mathf.Epsilon)
                {
                    animator.SetBool(Animator.StringToHash("run"), true);
                    return;
                }
                return;
            }
            if (state == Animator.StringToHash("Run"))
            {
                m_lockMove = false;
                // to Idle
                if (rigidbody2D.velocity.magnitude <= Mathf.Epsilon)
                {
                    animator.SetBool(Animator.StringToHash("run"), false);
                    rigidbody2D.velocity = Vector2.zero;
                    return;
                }
                return;
            }
            if (state == Animator.StringToHash("Stamp"))
            {
                m_lockMove = true;
                return;
            }
            if (state == Animator.StringToHash("Idle"))
                return;
            if (state == Animator.StringToHash("Idle"))
                return;
            if (state == Animator.StringToHash("Idle"))
                return;
            if (state == Animator.StringToHash("Idle"))
                return;
        }

        private void Stamp()
        {
            //if (m_targetPlayer == null)
            //    return;
            animator.SetBool(Animator.StringToHash("stamp"), true);
            if (m_targetPlayer != null)
            {
                GameManager.Instance.DelayDo(() =>
                {
                    Camera.main.GetComponent<CameraShake>().Shake();
                }, animator.GetCurrentAnimatorStateInfo(0).length * 0.5f);
            }
            GameManager.Instance.DelayDo(() =>
            {
                StampOther(m_targetPlayer);
                if (this == null)
                    return;
                animator.SetBool(Animator.StringToHash("stamp"), false);
            }, animator.GetCurrentAnimatorStateInfo(0).length * 0.75f);
        }

        public void Mute()
        {
            m_mute = true;
        }

        public void Wake()
        {
            m_mute = false;
        }

        public void StampOther(PlayerController other)
        {
            if (other != null)
            {
                AudioManager.Instance.PlaySoundEffect("stamp");
                AudioManager.Instance.PlaySoundEffect("win cheer");
                ChessData.Instance.Score((int)playerType);
            }
        }

        public void ResetPlayer()
        {
            m_mute = false;
            animator.Play("Idle");
        }

        //private void OnTriggerEnter2D(Collider2D collision)
        //{
        //    if (collision.tag == "LongShadow" && collision.transform.parent.parent != transform)
        //    {
        //        m_targetPlayer = collision.transform.parent.parent.GetComponent<PlayerController>();
        //    }
        //}
        //private void OnTriggerExit2D(Collider2D collision)
        //{
        //    if (collision.tag == "LongShadow" && collision.transform.parent.parent != transform)
        //    {
        //        m_targetPlayer = null;
        //    }
        //}
    }
}
