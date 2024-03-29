﻿using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code.Menus.ConnectionMenu
{
    class StartButton : MonoBehaviour
    {
        [SerializeField]
        private ConnectionModel _connectionModel;

        public void StartGame()
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            if (!PhotonNetwork.OfflineMode && PhotonNetwork.PlayerList.Length <= 0)
                return;

            _connectionModel.StartGame();
        }
    }
}
