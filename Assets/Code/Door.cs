using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    class Door : MonoBehaviour
    {
        public void Open()
        {
            StartCoroutine(SlideOpen());
        }

        private IEnumerator SlideOpen()
        {            
            while (transform.position.y > -1)
            {
                transform.position += new Vector3(0, -0.1f, 0);
                yield return null;                
            }
        }

    }
}
