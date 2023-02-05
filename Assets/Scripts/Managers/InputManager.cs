using Lfish.Pattern;
using UnityEngine;

namespace Lfish
{
    public class InputManager : SingletonCommon<InputManager>
    {
        private bool m_lockX = false;
        private bool m_lockY = false;

        public bool lockX
        {
            set => m_lockX = value;
            get => m_lockX;
        }
        public bool lockY
        {
            set => m_lockY = value;
            get => m_lockY;
        }
        //public float axisX
        //{
        //    get => m_lockX ? 0 : ETCInput.GetAxis("Horizontal");
        //}

        //public float axisY
        //{
        //    get => m_lockY ? 0 : ETCInput.GetAxis("Vertical");
        //}

        //public UnityEngine.Vector2 direction
        //{
        //    get => (new Vector2(axisX, axisY)).normalized;
        //}

        public Vector2 GetAxis(int playerIndex, bool normalize = true)
        {
            float axisX = Input.GetAxis($"Horizontal{playerIndex}");
            float axisY = Input.GetAxis($"Vertical{playerIndex}");
            return normalize ? new Vector2(axisX, axisY) : new Vector2(axisX, axisY).normalized;
        }

        public bool GetStamp(int playerIndex)
        {
            return Input.GetButtonDown($"Stamp{playerIndex}");
        }
    }
}
