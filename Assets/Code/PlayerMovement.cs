using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(Rigidbody))]
    class PlayerMovement : MonoBehaviour
    {
        [SerializeField]private float _speed = 5;
        private Vector3 _direction;
        private Rigidbody _rb;
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        }
        private void FixedUpdate()
        {
            _rb.MovePosition(transform.position += _direction * _speed * Time.fixedDeltaTime);
            
        }

    }
}
