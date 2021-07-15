using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code.Utilities
{
    class PingSphere : MonoBehaviour
    {
        private float _targetScale = 3f;
        private float _scaleMultiplier = 1.05f;
        private Vector3 _scaleVector;
        private Vector3 _startScale = new Vector3(.1f, .1f, .1f);
        private MeshRenderer _renderer;
        private Camera _cam;
        private void Awake()
        {
            _cam = FindObjectOfType<Camera>();
            _renderer = GetComponent<MeshRenderer>();
            _renderer.enabled = !_renderer.enabled;
            _scaleVector = new Vector3(_scaleMultiplier, _scaleMultiplier, _scaleMultiplier);
            transform.localScale = _startScale;
        }

        private void Update()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = Camera.main.nearClipPlane;
            Vector3 worldPosition = _cam.ScreenToWorldPoint(mousePos);
            Vector3 spawnPosition = new Vector3(worldPosition.x, 0, worldPosition.z);
            print(spawnPosition);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Ping(spawnPosition));

            }
        }

        private IEnumerator Ping(Vector3 position)
        {
            transform.position = position;
            _renderer.enabled = _renderer.enabled;
            while (transform.localScale.x <= _targetScale)
            {
                transform.localScale += _scaleVector;
                yield return new WaitForSeconds(.05f);
            }
            _renderer.enabled = !_renderer.enabled;
            transform.localScale = _startScale;

            yield return null;
        }

    }
}
