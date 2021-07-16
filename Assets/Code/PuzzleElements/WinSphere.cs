using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OniosNetworKing.Assets.Code
{
    class WinSphere : MonoBehaviourPunCallbacks
    {
        private bool _playersWon = false;
        private List<Player> _playersTouchingMe = new List<Player>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                if (!_playersTouchingMe.Contains(player))
                {
                    _playersTouchingMe.Add(player);
                }
            }
        }

        private void Update()
        {
            if (_playersTouchingMe.Count >= 2)
            {
                TriggerWin();
            }
        }
        private void TriggerWin()
        {
            if (!_playersWon)
            {
                PhotonNetwork.LoadLevel(3);
                _playersWon = true;
            }
        }

    }
}
