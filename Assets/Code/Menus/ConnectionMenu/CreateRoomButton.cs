using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

namespace OniosNetworKing.Assets.Code.Menus.ConnectionMenu
{
    class CreateRoomButton : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField _roomNameInput;
        [SerializeField]
        private ConnectionModel _connectionModel;
        [SerializeField]
        private StatusBar _statusDisplay;

        public void CreateRoomWithNameFromInput()  
        {
            if (!PhotonNetwork.IsConnected)
                return;

            if (_connectionModel.GetAllRooms().Count >= 1)
            {
                _statusDisplay.WriteToStatusBar("youve already got a room fool, don't get greedy");
                return;
            }

            _connectionModel.CreateRoom(_roomNameInput.text);
            _statusDisplay.WriteToStatusBar($"Room Created With Name {_roomNameInput.text}");
        }
    }
}
