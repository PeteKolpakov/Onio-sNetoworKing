using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

        private void OnError(string message)
        {
            if (_textDisplay != null)
            {
                _textDisplay.text = "Status: ERROR: " + message;
            }
        }
    }
}
