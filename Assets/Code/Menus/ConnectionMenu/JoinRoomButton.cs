using OniosNetworKing.Assets.Code.ConnectionMenu;
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
        [SerializeField]
        private StatusBar _statusBar;


        public string GetDestinationRoomName()
        {
            if (_connectionModel.GetLastRoomCreated() != null)
            {
                return _connectionModel.GetLastRoomCreated().Name;
            }
            else return null;
        }

        public void JoinRoom()
        {
            if (GetDestinationRoomName() != null)
            {
                _connectionModel.JoinRoom(GetDestinationRoomName());
            }
            else
            {
                _statusBar.WriteToStatusBar("Error: No Room Available To Join");
            }

        }
    }
}
