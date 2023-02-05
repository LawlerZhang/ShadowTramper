using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lfish.Pattern;

namespace Lfish
{
    public class RandomPointInScene : SingletonMono<RandomPointInScene>
    {
        [System.Serializable]
        public class MaskPoint
        {
            public Vector2 bottomLeft;
            public Vector2 upRight;
        }
        [SerializeField]
        private MaskPoint[] m_maskPoints;
        [SerializeField]
        private Vector2 m_bottomLeft;
        [SerializeField]
        private Vector2 m_upRight;

        private new void Awake()
        {
            base.Awake();
        }

        protected override void BeforeBaseInit()
        {
            m_instance = this;
        }

        public Vector2 RandomPosition()
        {
            float randomX = 0f;
            float randomY = 0f;
            for (int i = 0; i < 100; ++i)
            {
                randomX = Random.Range(m_bottomLeft.x, m_upRight.x);
                randomY = Random.Range(m_bottomLeft.y, m_upRight.y);
                if (LegalPosition(randomX, randomY))
                    break;
            }
            return new Vector2(randomX, randomY);
        }

        public bool LegalPosition(float x, float y)
        {
            foreach (var rect in m_maskPoints)
            {
                if (x > rect.bottomLeft.x && x < rect.upRight.x && y > rect.bottomLeft.y && y < rect.upRight.y)
                    return false;
            }
            return true;
        }
#if DEBUG
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            int index = 0;
            foreach (var rect in m_maskPoints)
            {
                if (rect.bottomLeft.x > rect.upRight.x || rect.bottomLeft.y > rect.upRight.y)
                    Debug.LogError($"Element{index}, Illegal rect: bottomLeft({rect.bottomLeft.x}, {rect.bottomLeft.y}), upRight({rect.upRight.x}, {rect.upRight.y})");
                Gizmos.DrawLine(new Vector2(rect.bottomLeft.x, rect.bottomLeft.y), new Vector2(rect.bottomLeft.x, rect.upRight.y));
                Gizmos.DrawLine(new Vector2(rect.bottomLeft.x, rect.upRight.y), new Vector2(rect.upRight.x, rect.upRight.y));
                Gizmos.DrawLine(new Vector2(rect.upRight.x, rect.upRight.y), new Vector2(rect.upRight.x, rect.bottomLeft.y));
                Gizmos.DrawLine(new Vector2(rect.upRight.x, rect.bottomLeft.y), new Vector2(rect.bottomLeft.x, rect.bottomLeft.y));
                ++index;
            }
        }
#endif
    }
}
