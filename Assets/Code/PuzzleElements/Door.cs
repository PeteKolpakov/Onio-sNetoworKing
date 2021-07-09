using OniosNetworKing.Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(Collider))]
    class Door : SwitchPart
    {
        private Vector3 _initialPosition;
        private Collider _collider;
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _initialPosition = transform.position;
        }
        public void Open()
        {
            _collider.enabled = false;
            StartCoroutine(DUtils.SlideDown(gameObject));
        }
        public void Close()
        {
            _collider.enabled = true;
            StartCoroutine(DUtils.SlideUpTo(gameObject, _initialPosition, .2f));
        }
    }
}
