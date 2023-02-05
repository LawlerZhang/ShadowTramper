using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Lfish
{
    public class Shadow : MonoBehaviour
    {
        public Transform longShadow;

        public void SetRotation(Transform light)
        {
            if (light == null)
            {
                transform.localEulerAngles = Vector3.zero;
                return;
            }
            Vector3 parentPosition = transform.parent.position;
            float dot = Vector2.Dot(new Vector2(parentPosition.x - light.position.x, parentPosition.y - light.position.y).normalized, Vector2.right);
            float angle = Mathf.Acos(dot) * Mathf.Rad2Deg;
            if (parentPosition.y < light.position.y)
                angle = -angle;
            transform.localEulerAngles = new Vector3(0, 0, angle);
        }
    }
}
