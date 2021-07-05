using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace OniosNetworKing.Assets.Code
{
    class WinSphere : MonoBehaviour
    {
        private bool _playerOneWon = false;
        private bool _playerTwoWon = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
            {
                if (player.GetPlayerID() == 0)
                {
                    _playerOneWon = true;
                }
                if (player.GetPlayerID() == 1)
                {
                    _playerTwoWon = true;
                }
            }
        }

        private void Update()
        {
            if (_playerTwoWon && _playerTwoWon)
            {
                TriggerWin();
            }
        }
        private void TriggerWin()
        {
            SceneManager.LoadScene(2);
        }

    }
}
