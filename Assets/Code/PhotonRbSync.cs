using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
/// <summary>
/// inspiration taken from https://sharpcoderblog.com/blog/sync-rigidbodies-over-network-using-pun-2
/// </summary>
namespace OniosNetworKing.Assets.Code
{
    class PhotonRbSync : MonoBehaviourPun, IPunObservable
    {
        private Rigidbody _rb;

        private Vector3 _latestPos;
        private Vector3 _velocity;

        private bool _valuesReceived = false;
        
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(_rb.velocity);
            }
            else
            {
                _latestPos = (Vector3)stream.ReceiveNext();
                _velocity = (Vector3)stream.ReceiveNext();

                _valuesReceived = true;
            }
        }
        
        void Update()
        {
            if (!photonView.IsMine && _valuesReceived)
            {
                transform.position = Vector3.Lerp(transform.position, _latestPos, Time.deltaTime * 5);
                _rb.velocity = _velocity;
            }
        }

        void OnCollisionEnter(Collision contact)
        {
            if (!photonView.IsMine)
            {
                Transform collisionObjectRoot = contact.transform.root;
                if (TryGetComponent<Player>(out Player player))
                {
                    photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
                }
            }
        }
    }
}

