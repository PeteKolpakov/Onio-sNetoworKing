using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace OniosNetworKing.Assets.Code.Menus.ConnectionMenu
{
    class StatusBar : MonoBehaviour
    {
        [SerializeField]
        private ConnectionModel _connectionModel;
        private TextMeshProUGUI _textDisplay;

        private void Start()
        {
            _connectionModel.ConnectionError += OnError;
            _textDisplay = GetComponent<TextMeshProUGUI>();
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
