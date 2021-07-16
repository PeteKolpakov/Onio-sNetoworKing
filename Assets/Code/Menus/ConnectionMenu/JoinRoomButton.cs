using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OniosNetworKing.Assets.Code.Menus.ConnectionMenu
{
    class JoinRoomButton : MonoBehaviour
    {
        [SerializeField]
        private ConnectionModel _connectionModel;
        public void JoinRoom()
        {
            _connectionModel.JoinRandomRoom();
        }

    }
}
