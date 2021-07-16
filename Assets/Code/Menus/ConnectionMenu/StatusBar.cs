using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace OniosNetworKing.Assets.Code.Menus.ConnectionMenu
{
    class StatusBar : MonoBehaviourPunCallbacks
    {
        [SerializeField]
        private ConnectionModel _connectionModel;
        private TextMeshProUGUI _textDisplay;

        private void Start()
        {
            _connectionModel.ConnectionError += OnError;
            _textDisplay = GetComponent<TextMeshProUGUI>();
        }

        private void OnError(string obj)
        {
            _textDisplay.text = "State: Error" + obj;
        }

        private void Update()
        {
            if (_textDisplay != null)
            {
                _textDisplay.text = "State: " + PhotonNetwork.NetworkClientState;
            }
        }
    }
}
