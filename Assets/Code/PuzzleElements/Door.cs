using OniosNetworKing.Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    class Door : SwitchPart
    {
        private Vector3 _initialPosition;
        private void Awake()
        {
            _initialPosition = transform.position;
        }
        public void Open()
        {
            StartCoroutine(DUtils.SlideDown(gameObject));
        }
        public void Close()
        {
            StartCoroutine(DUtils.SlideUpTo(gameObject, _initialPosition, .2f));
        }
    }
}
