﻿using OniosNetworKing.Assets.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace Assets.Code
{
    [RequireComponent(typeof(Player))]
    [RequireComponent(typeof(Rigidbody))]
    class PlayerMovement : MonoBehaviourPun, IPunObservable
    {        
        [SerializeField]private float _speed = 5;
        private Vector3 _direction;
        private Rigidbody _rb;
        private Player _player;
        private string _xAxisInput;
        private string _yAxisInput;

        [SerializeField] TMP_Text Nickname;

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
            Nickname.text = photonView.Owner.NickName;
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(photonView.IsMine){
                _direction = new Vector3(Input.GetAxisRaw(_xAxisInput), 0, Input.GetAxisRaw(_yAxisInput)).normalized;
            }
        }
        private void FixedUpdate()
        {
            if (OniosNetworKing.ChatManager.IsChatting)
            {
                return;
            }
            _rb.velocity = _direction * _speed;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // Sending our input through here

            stream.SendNext(_direction);
        }
        else
        {
            // Receiving inputs from other players

            _direction = (Vector3)stream.ReceiveNext();
        }
    }

    }
}
