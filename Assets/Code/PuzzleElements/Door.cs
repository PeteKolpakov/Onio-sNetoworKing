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
        [SerializeField] private bool _startsOpen;
        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _initialPosition = transform.position;
            if (_startsOpen)
            {
                StartCoroutine(DUtils.SlideDown(gameObject));
            }
        }
        public void Open()
        {
            if (!_startsOpen)
            {
                _collider.enabled = false;
                StartCoroutine(DUtils.SlideDown(gameObject));
            }
            else if (_startsOpen)
            {
                _collider.enabled = true;
                StartCoroutine(DUtils.SlideUpTo(gameObject, _initialPosition, .2f));
            }
        }
        public void Close()
        {
            if (!_startsOpen)
            {
                _collider.enabled = true;
                StartCoroutine(DUtils.SlideUpTo(gameObject, _initialPosition, .2f));
            }
            else if (_startsOpen)
            {
                _collider.enabled = false;
                StartCoroutine(DUtils.SlideDown(gameObject));
            }

        }
    }
}
