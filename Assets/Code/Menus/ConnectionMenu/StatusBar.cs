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
        [SerializeField]
        private TextMeshProUGUI _targetTextDisplay;

        private void Start()
        {
            _connectionModel.ConnectionError += OnError;
        }

        private void OnError(string obj)
        {
            _targetTextDisplay.text = "State: Error" + obj;
        }

        public void WriteToStatusBar(string message)
        {
            _targetTextDisplay.text = "State: " + message; 
        }

        private void Update()
        {            
            _targetTextDisplay.SetText("State: " + PhotonNetwork.NetworkClientState.ToString());           
        }
    }
}
