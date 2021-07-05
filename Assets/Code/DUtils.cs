using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code
{
    public static class DUtils 
    {
        private const float SLIDEDOWN_SPEED = 0.1f;
        private const float GROUND_OFFSET = 0.1f;

        /// <summary>
        /// Causes target gameobject to slide down untill it is 1/2 of its y scale below ground 
        /// (if ground is 0)
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static IEnumerator SlideDown(GameObject target)
        {
            while (target.transform.position.y > -(target.transform.localScale.y / 2) + GROUND_OFFSET)
            {
                target.transform.position += new Vector3(0, -SLIDEDOWN_SPEED, 0);
                yield return null;
            }
        }

    }
}
