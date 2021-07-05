using OniosNetworKing.Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    class PlayerMovement : MonoBehaviour
    {        
        [SerializeField]private float _speed = 5;
        private Vector3 _direction;
        private Rigidbody _rb;
        private Player _player;
        private string _xAxisInput;
        private string _yAxisInput;

        private void Awake()
        {
            _player = GetComponent<Player>();
            if (_player.GetPlayerID() == 0)
            {
                _xAxisInput = "Horizontal_P1";
                _yAxisInput = "Vertical_P1";
            }
            else if (_player.GetPlayerID() == 1)
            {
                _xAxisInput = "Horizontal_P2";
                _yAxisInput = "Vertical_P2";
            }
        }
        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            _direction = new Vector3(Input.GetAxisRaw(_xAxisInput), 0, Input.GetAxisRaw(_yAxisInput)).normalized;
        }
        private void FixedUpdate()
        {
            _rb.MovePosition(transform.position += _direction * _speed * Time.fixedDeltaTime);
            
        }

    }
}
