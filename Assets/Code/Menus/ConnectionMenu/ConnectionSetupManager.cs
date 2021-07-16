using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code.Menus.ConnectionMenu
{
    class ConnectionSetupManager : MonoBehaviour
    {
        [SerializeField]
        private ConnectionModel _connectionModel;

        private void Awake()
        {
            _connectionModel.ConnectToServer();

        }

    }
}
